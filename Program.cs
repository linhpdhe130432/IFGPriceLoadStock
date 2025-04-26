using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using BackgroundServiceDemo.Models;
using BackgroundServiceDemo.Services;
using Microsoft.Extensions.Logging;

namespace BackgroundServiceDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService() // This enables Windows Service support
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<FileWatcherSettings>(
                        hostContext.Configuration.GetSection("FileWatcher"));

                    services.AddSingleton<OracleStockService>();

                    services.AddSingleton<FixFileProcessorService>();

                    services.AddHostedService<WorkerService>();
                });
    }
} 