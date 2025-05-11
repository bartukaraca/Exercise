using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Exercise.Application.Abstractions.Services;
using Exercise.Application.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace Exercise.Persistence.Services
{
    public class JsonFileWatcherService
    {
        private readonly string _folderPath;
        private readonly IServiceProvider _serviceProvider;

        public JsonFileWatcherService(IServiceProvider serviceProvider)
        {
            // Belirli klasör yolu
            _folderPath = @"C:\Users\brt_k\Desktop\Bitirme\received_detections";
            _serviceProvider = serviceProvider;
            Directory.CreateDirectory(_folderPath);
        }

        public void StartWatching()
        {
            FileSystemWatcher watcher = new FileSystemWatcher(_folderPath, "*.json");
            watcher.Created += OnJsonFileCreated;
            watcher.EnableRaisingEvents = true;
            Console.WriteLine("📂 JsonFileWatcherService başlatıldı ve dosyalar izleniyor...");
        }

        private async void OnJsonFileCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                await Task.Delay(100); // Dosyanın tamamen yazılmasını beklemek
                var jsonContent = await File.ReadAllTextAsync(e.FullPath);
                var roadDto = JsonSerializer.Deserialize<RoadDto>(jsonContent);

                if (roadDto != null)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var roadService = scope.ServiceProvider.GetRequiredService<IRoadService>();
                        await roadService.SaveOrUpdateRoadAsync(roadDto);
                    }
                }

                // İşlendikten sonra dosyayı taşı
                string processedPath = Path.Combine(_folderPath, "Processed");
                Directory.CreateDirectory(processedPath);
                File.Move(e.FullPath, Path.Combine(processedPath, Path.GetFileName(e.FullPath)));

                Console.WriteLine($"✅ İşlendi ve taşındı: {e.FullPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Hata: {ex.Message}");
            }
        }
    }
}
