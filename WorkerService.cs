using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BackgroundServiceDemo.Services;
using BackgroundServiceDemo.Models;

namespace BackgroundServiceDemo
{
    public class WorkerService : BackgroundService
    {
        private readonly ILogger<WorkerService> _logger;
        private readonly FixFileProcessorService _fixFileProcessorService;

        public WorkerService(
            ILogger<WorkerService> logger,
            FixFileProcessorService fixFileProcessorService)
        {
            _logger = logger;
            _fixFileProcessorService = fixFileProcessorService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {

                while (!stoppingToken.IsCancellationRequested)
                {
                    _fixFileProcessorService.StartWatching();
                    await Task.Delay(1000, stoppingToken);
                }
            }
            finally
            {
                _fixFileProcessorService.StopWatching();
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping service...");
            _fixFileProcessorService.StopWatching();
            await base.StopAsync(cancellationToken);
        }
    }
} 