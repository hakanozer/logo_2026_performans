using Microsoft.Extensions.Logging;

namespace RestApi.Logging
{
    public class InfluxDbLogger : ILogger
    {
        private readonly string _category;
        private readonly InfluxLineProtocolWriter _writer;

        public InfluxDbLogger(string category, InfluxLineProtocolWriter writer)
        {
            _category = category;
            _writer = writer;
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull => NullScope.Instance;

        public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Information;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var message = formatter(state, exception);
            var fields = new Dictionary<string, object?>();
            if (state is IEnumerable<KeyValuePair<string, object>> pairs)
            {
                foreach (var pair in pairs)
                {
                    fields[pair.Key] = pair.Value;
                }
            }

            _writer.Write(_category, logLevel, message, exception, fields);
        }

        private sealed class NullScope : IDisposable
        {
            public static readonly NullScope Instance = new();
            public void Dispose() { }
        }
    }
}
