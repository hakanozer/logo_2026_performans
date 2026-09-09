namespace RestApi.Logging
{
    public class InfluxDbOptions
    {
        public string Url { get; set; } = "http://localhost:8086";
        public string Database { get; set; } = "jmeter";
        public string Measurement { get; set; } = "app_logs";
    }
}
