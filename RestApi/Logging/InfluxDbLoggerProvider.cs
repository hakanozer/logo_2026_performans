using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace RestApi.Logging
{
    public class InfluxDbLoggerProvider : ILoggerProvider
    {
        private readonly InfluxLineProtocolWriter _writer;
        private readonly ConcurrentDictionary<string, InfluxDbLogger> _loggers = new();

        public InfluxDbLoggerProvider(InfluxLineProtocolWriter writer)
        {
            _writer = writer;
        }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new InfluxDbLogger(name, _writer));

        public void Dispose() => _loggers.Clear();
    }
}
