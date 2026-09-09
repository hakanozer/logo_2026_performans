using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace RestApi.Logging
{
    // Fire-and-forget writer that ships log entries to InfluxDB using the line protocol.
    public class InfluxLineProtocolWriter
    {
        private readonly HttpClient _httpClient;
        private readonly InfluxDbOptions _options;

        public InfluxLineProtocolWriter(IOptions<InfluxDbOptions> options)
        {
            _options = options.Value;
            _httpClient = new HttpClient { BaseAddress = new Uri(_options.Url) };
        }

        public void Write(string category, LogLevel level, string message, Exception? exception, IReadOnlyDictionary<string, object?> fields)
        {
            // Never let telemetry shipping break the request pipeline.
            _ = SendAsync(BuildLine(category, level, message, exception, fields));
        }

        // Low-cardinality keys promoted to tags so Grafana can GROUP BY them (InfluxQL only groups on tags).
        private static readonly string[] TagPromotedKeys = { "Method", "StatusCode", "Path" };

        private string BuildLine(string category, LogLevel level, string message, Exception? exception, IReadOnlyDictionary<string, object?> fields)
        {
            var tags = new StringBuilder();
            tags.Append(_options.Measurement);
            tags.Append(",level=").Append(EscapeTag(level.ToString()));
            tags.Append(",category=").Append(EscapeTag(category));

            var fieldParts = new List<string> { $"message={EscapeFieldString(message)}" };
            if (exception is not null)
            {
                fieldParts.Add($"exception={EscapeFieldString(exception.GetType().Name + ": " + exception.Message)}");
            }

            foreach (var (key, value) in fields)
            {
                if (value is null || key == "{OriginalFormat}")
                {
                    continue;
                }

                if (Array.IndexOf(TagPromotedKeys, key) >= 0)
                {
                    tags.Append(',').Append(SanitizeKey(key)).Append('=').Append(EscapeTag(value.ToString() ?? string.Empty));
                    continue;
                }

                fieldParts.Add($"{SanitizeKey(key)}={FormatFieldValue(value)}");
            }

            var timestampNs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() * 1_000_000L;
            return $"{tags} {string.Join(',', fieldParts)} {timestampNs}";
        }

        private async Task SendAsync(string line)
        {
            try
            {
                var content = new StringContent(line);
                var response = await _httpClient.PostAsync(
                    $"/write?db={Uri.EscapeDataString(_options.Database)}&precision=ns", content);

                if (!response.IsSuccessStatusCode)
                {
                    Console.Error.WriteLine($"InfluxDB write failed with status {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"InfluxDB write failed: {ex.Message}");
            }
        }

        private static string EscapeTag(string value) =>
            value.Replace(" ", "\\ ").Replace(",", "\\,").Replace("=", "\\=");

        private static string EscapeFieldString(string value) =>
            "\"" + value.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";

        private static string SanitizeKey(string key) =>
            key.Replace(" ", "_").Replace(",", "_").Replace("=", "_");

        private static string FormatFieldValue(object value) => value switch
        {
            bool b => b ? "true" : "false",
            int or long or short => Convert.ToInt64(value, CultureInfo.InvariantCulture) + "i",
            float or double or decimal => Convert.ToDouble(value, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture),
            _ => EscapeFieldString(value.ToString() ?? string.Empty)
        };
    }
}
