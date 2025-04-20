using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercise.Persistence.Services;
using Microsoft.Extensions.Hosting;

namespace Exercise.Persistence.Services
{
    public class WatcherHostedService : IHostedService
    {
        private readonly JsonFileWatcherService _jsonFileWatcherService;

        public WatcherHostedService(JsonFileWatcherService jsonFileWatcherService)
        {
            _jsonFileWatcherService = jsonFileWatcherService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _jsonFileWatcherService.StartWatching();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("🚦 JsonFileWatcherService durduruluyor...");
            return Task.CompletedTask;
        }
    }
}