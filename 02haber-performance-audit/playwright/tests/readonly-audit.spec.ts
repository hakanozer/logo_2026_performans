import { test, expect } from '@playwright/test';

test('homepage responds and captures browser diagnostics', async ({ page }) => {
  const consoleErrors: string[] = [];
  const pageErrors: string[] = [];
  const failedRequests: string[] = [];
  const badResponses: Array<{ url: string; status: number }> = [];

  page.on('console', msg => {
    if (msg.type() === 'error') consoleErrors.push(msg.text());
  });
  page.on('pageerror', error => pageErrors.push(error.message));
  page.on('requestfailed', request => {
    failedRequests.push(`${request.method()} ${request.url()} :: ${request.failure()?.errorText}`);
  });
  page.on('response', response => {
    if (response.status() >= 400) {
      badResponses.push({ url: response.url(), status: response.status() });
    }
  });

  const response = await page.goto('/', { waitUntil: 'domcontentloaded' });
  expect(response, 'Navigation should produce a response').not.toBeNull();

  const navigation = await page.evaluate(() => {
    const entry = performance.getEntriesByType('navigation')[0] as PerformanceNavigationTiming;
    return {
      responseStart: Math.round(entry.responseStart),
      domInteractive: Math.round(entry.domInteractive),
      domContentLoaded: Math.round(entry.domContentLoadedEventEnd),
      loadEventEnd: Math.round(entry.loadEventEnd),
      transferSize: entry.transferSize
    };
  });

  await expect(page.locator('body')).toBeVisible();

  const title = await page.title();
  const linkCount = await page.locator('a[href]').count();

  await test.info().attach('homepage-audit.json', {
    body: Buffer.from(JSON.stringify({
      url: page.url(),
      httpStatus: response!.status(),
      title,
      linkCount,
      navigation,
      consoleErrors,
      pageErrors,
      failedRequests,
      badResponses
    }, null, 2)),
    contentType: 'application/json'
  });

  console.log(JSON.stringify({
    httpStatus: response!.status(),
    title,
    linkCount,
    navigation,
    consoleErrors,
    pageErrors,
    failedRequests,
    badResponses
  }, null, 2));

  expect(response!.status()).toBeLessThan(400);
});

test('discovers a limited sample of same-origin links without submitting forms', async ({ page, context }) => {
  const base = new URL(process.env.BASE_URL || 'https://02haber.com/');
  await page.goto('/', { waitUntil: 'domcontentloaded' });

  const hrefs = await page.locator('a[href]').evaluateAll(anchors =>
    anchors.map(a => (a as HTMLAnchorElement).href)
  );

  const candidates = [...new Set(hrefs)]
    .filter(href => {
      try {
        const url = new URL(href);
        return url.origin === base.origin &&
          ['http:', 'https:'].includes(url.protocol) &&
          !url.pathname.match(/\.(pdf|jpg|jpeg|png|gif|zip|mp4)$/i);
      } catch {
        return false;
      }
    })
    .slice(0, 5);

  const results: Array<{ url: string; status: number | null; error?: string }> = [];

  for (const url of candidates) {
    const response = await context.request.get(url, { timeout: 15000 });
    results.push({ url, status: response.status() });
  }

  await test.info().attach('same-origin-link-sample.json', {
    body: Buffer.from(JSON.stringify(results, null, 2)),
    contentType: 'application/json'
  });

  console.log('Same-origin read-only link sample:', results);
});
