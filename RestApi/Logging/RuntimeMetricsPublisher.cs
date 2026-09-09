using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestApi.Data;

namespace RestApi.Logging
{
    // Periodically logs process CPU/RAM/GC/DB-connection metrics so they flow into
    // InfluxDB through InfluxDbLoggerProvider, same as request/error logs.
    public class RuntimeMetricsPublisher : BackgroundService
    {
        private static readonly TimeSpan SampleInterval = TimeSpan.FromSeconds(10);
        private readonly ILogger _logger;
        private TimeSpan _lastCpuTime;
        private DateTime _lastSampleTimeUtc;

        public RuntimeMetricsPublisher(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("RuntimeMetrics");
            var process = Process.GetCurrentProcess();
            _lastCpuTime = process.TotalProcessorTime;
            _lastSampleTimeUtc = DateTime.UtcNow;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(SampleInterval);
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                PublishSnapshot();
            }
        }

        private void PublishSnapshot()
        {
            var process = Process.GetCurrentProcess();
            var nowUtc = DateTime.UtcNow;
            var currentCpuTime = process.TotalProcessorTime;
            var elapsedMs = (nowUtc - _lastSampleTimeUtc).TotalMilliseconds;

            var cpuPercent = 0d;
            if (elapsedMs > 0)
            {
                var cpuDeltaMs = (currentCpuTime - _lastCpuTime).TotalMilliseconds;
                cpuPercent = Math.Round(cpuDeltaMs / (elapsedMs * Environment.ProcessorCount) * 100, 2);
            }

            _lastCpuTime = currentCpuTime;
            _lastSampleTimeUtc = nowUtc;

            var workingSetMb = Math.Round(process.WorkingSet64 / 1024d / 1024d, 1);
            var gcHeapMb = Math.Round(GC.GetTotalMemory(false) / 1024d / 1024d, 1);

            _logger.LogInformation(
                "Runtime metrics snapshot: CPU {CpuPercent} RAM {WorkingSetMb} GcHeap {GcHeapMb} Threads {ThreadCount} Gen0 {Gen0Collections} Gen1 {Gen1Collections} Gen2 {Gen2Collections} DbActiveConnections {DbActiveConnections}",
                cpuPercent, workingSetMb, gcHeapMb, process.Threads.Count,
                GC.CollectionCount(0), GC.CollectionCount(1), GC.CollectionCount(2),
                ApplicationDbContext.ActiveConnectionCount);
        }
    }
}
