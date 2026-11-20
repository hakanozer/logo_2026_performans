# 02Haber – kontrollü performans denetimi paketi

**Durum:** Test paketi / başlangıç iskeleti. Bu paket çalıştırılmış test sonuçları içermez.

## Önemli üretim güvenliği

Hedef `https://02haber.com/` canlı üretim sitesidir. Sunucu CPU/RAM/DB izlemesi olmadığı ve kapasite bilinmediği için bu paket varsayılan olarak yalnızca düşük hacimli, salt-okunur smoke/baseline kontrolleri için hazırlanmıştır.

- İlk çalıştırmada yalnızca 1 JMeter sanal kullanıcısı, 1 döngü ve tek ana sayfa GET isteği kullanın.
- Her aşamadan sonra durup HTTP hata oranını, gecikmeyi ve site davranışını inceleyin.
- Üretimde 5/10/25/50 kullanıcıya çıkmayı otomatikleştirmeyin. Site barındırma sağlayıcısı/CDN/WAF ve operasyon ekibinden yazılı onay ve test penceresi alın.
- Kapasite keşfi, sunucu gözlemi olmadan production üzerinde yapılmamalıdır.
- POST/PUT/DELETE, login, arama formu, yorum, abonelik, ödeme ve dosya yükleme işlemleri bu pakette otomatikleştirilmez.
- CAPTCHA, WAF, rate limit veya erişim kontrollerini aşmaya çalışmayın.
- Testte beklenmeyen 429/403/5xx, belirgin gecikme artışı veya kullanıcıya yansıyan sorun görülürse testi durdurun.
- JMeter ve Playwright yükünü aynı anda başlatmayın. Önce düşük hacimli HTTP smoke, sonra sınırlı tarayıcı kontrolü yapın.

## İçerik

- `jmeter/02haber-readonly-smoke.jmx`: tek ana sayfa GET isteği için düşük hacimli JMeter planı.
- `playwright/`: Chromium üzerinde salt-okunur tarayıcı kontrolleri, performans zamanlamaları, console/page error ve temel aynı-origin link keşfi.
- `.env.example`: hedef URL örneği.
- Playwright testleri otomatik yük testi değildir; az sayıda tarayıcı oturumuyla sentetik kullanıcı deneyimi kontrolüdür.

## Gereksinimler

- Java 17+ (JMeter sürümünüzün gereksinimini kontrol edin)
- Apache JMeter 5.6.x veya kurumunuzca onaylanan sürüm
- Node.js LTS
- npm

## 1) Playwright kurulumu

```bash
cd playwright
npm install
npx playwright install chromium
cp .env.example .env
```

`.env` içinde `BASE_URL=https://02haber.com/` bırakılabilir.

Çalıştırma:

```bash
npm test
```

HTML raporu:

```bash
npx playwright show-report
```

JSON sonuçları `playwright/test-results/` altında oluşur.

## 2) JMeter – güvenli smoke testi

JMeter GUI ile `jmeter/02haber-readonly-smoke.jmx` dosyasını açıp önce parametreleri kontrol edin.

CLI:

```bash
jmeter -n \
  -t jmeter/02haber-readonly-smoke.jmx \
  -Jtarget_host=02haber.com \
  -Jprotocol=https \
  -Jthreads=1 \
  -Jramp_seconds=60 \
  -Jloops=1 \
  -Jthink_time_ms=5000 \
  -l results.jtl \
  -e -o jmeter-report
```

Bu plan yalnızca `GET /` isteği gönderir. Varsayılan tek kullanıcı/tek döngüdür. `results.jtl` ve `jmeter-report/` test çalıştırıldıktan sonra oluşur.

## 3) Aşamalı çalışma önerisi

| Aşama | Kullanıcı | Döngü | Üretim kararı |
|---|---:|---:|---|
| Smoke | 1 | 1 | Tek başına, düşük etkili kontrol |
| Baseline | 1 | 3 | Yalnızca smoke sorunsuzsa ve uygun zaman penceresinde |
| Mini gözlem | 2 | 3 | Hosting/CDN politikası kontrol edilip onay alındıktan sonra |
| Daha yüksek yük | 5+ | — | Sunucu gözlemi ve yazılı operasyon onayı olmadan çalıştırmayın |

Bu değerler kapasite tahmini veya SLA değildir. “Unknown capacity + no server monitoring” koşulunda 10/25/50/100 kullanıcı testleri production üzerinde önerilmez.

## 4) Sonuçları yorumlama

JMeter:
- `elapsed`: JMeter istemcisinin ölçtüğü örnek süresi (ms).
- `success`: HTTP assertion sonucuna göre başarı.
- `responseCode`: HTTP durum kodu.
- `bytes`: yanıt boyutu.

Playwright:
- `domContentLoaded`, `load`, `responseStart`, `domInteractive` gibi Navigation Timing alanları.
- HTTP response status dağılımı.
- JavaScript console ve uncaught page hataları.
- Link keşfi: yalnızca aynı-origin, GET tabanlı sayfalar; sınırlı sayıda.

Tarayıcı ölçümleri makinenin CPU/RAM'i, ağ, cache, tarayıcı sürümü ve CDN koşullarından etkilenir. Tek koşudaki değerleri kapasite garantisi olarak yorumlamayın.

## 5) Test sonrası

- `results.jtl`, `jmeter-report/`, `playwright/test-results/` ve `playwright/playwright-report/` çıktılarını saklayın.
- Test tarihini, commit/build bilgisini, cihazı, tarayıcı sürümünü, ağ koşullarını ve test penceresini rapora ekleyin.
- Gerçek kullanıcı verileri veya hassas response body'leri paylaşmayın.
- Test çıktıları yüklendiğinde sonuçlar üzerinden gerçek rapor hazırlanabilir.
