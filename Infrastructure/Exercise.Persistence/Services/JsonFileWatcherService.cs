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
            _folderPath = @"C:\Users\brt_k\Desktop\Bitirme\received_detections";
            _serviceProvider = serviceProvider;
            Directory.CreateDirectory(_folderPath);
        }

        public void StartWatching()
        {
            var watcher = new FileSystemWatcher(_folderPath, "*.json");
            watcher.Created += async (s, e) => await OnJsonFileCreated(e);
            watcher.EnableRaisingEvents = true;
            Console.WriteLine("📂 JsonFileWatcherService başlatıldı ve dosyalar izleniyor...");
        }

        private async Task OnJsonFileCreated(FileSystemEventArgs e)
        {
            try
            {
                await Task.Delay(100); // Dosyanın tam yazılmasını beklemek
                if (!File.Exists(e.FullPath)) return;

                var jsonContent = await File.ReadAllTextAsync(e.FullPath);
                var roadDto = JsonSerializer.Deserialize<RoadDto>(jsonContent);

                if (roadDto == null)
                {
                    Console.WriteLine("⚠️ Geçersiz JSON içeriği. RoadDto deserialize edilemedi.");
                    return;
                }

                using var scope = _serviceProvider.CreateScope();
                var roadService = scope.ServiceProvider.GetRequiredService<IRoadService>();
                var carService = scope.ServiceProvider.GetRequiredService<ICarService>();

                // Timestamp set et
                roadDto.timestamp = DateTime.UtcNow;

                // Road tablosunu güncelle/kaydet
                var updatedRoad = await roadService.SaveOrUpdateRoadAsync(roadDto);

                // Car bilgilerini JSON'dan al ve güncelle
                await UpdateCarsFromJson(jsonContent, updatedRoad.Id, carService);

                Console.WriteLine($"✅ Road güncellendi. Road ID: {updatedRoad.Id}, Status: {updatedRoad.RoadStatusId}");

                // Dosyayı işlenmiş klasöre taşı
                string processedPath = Path.Combine(_folderPath, "Processed");
                Directory.CreateDirectory(processedPath);
                string destinationFile = Path.Combine(processedPath, Path.GetFileName(e.FullPath));

                if (File.Exists(destinationFile))
                {
                    string fileName = Path.GetFileNameWithoutExtension(e.FullPath);
                    string extension = Path.GetExtension(e.FullPath);
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    destinationFile = Path.Combine(processedPath, $"{fileName}_{timestamp}{extension}");
                }

                if (File.Exists(e.FullPath))
                {
                    File.Move(e.FullPath, destinationFile);
                    Console.WriteLine($"✅ İşlendi ve taşındı: {destinationFile}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Hata: {ex.Message}");
                Console.WriteLine($"❌ Stack Trace: {ex.StackTrace}");
            }
        }

        private async Task UpdateCarsFromJson(string jsonContent, int roadId, ICarService carService)
        {
            try
            {
                using var document = JsonDocument.Parse(jsonContent);
                var root = document.RootElement;

                if (root.TryGetProperty("human_readable_id", out JsonElement humanReadableIdElement))
                {
                    string humanReadableId = humanReadableIdElement.GetString();
                    if (!string.IsNullOrEmpty(humanReadableId))
                    {
                        bool success = await carService.UpdateCarRoadIdByVehicleNumberAsync(humanReadableId, roadId);
                        if (success)
                        {
                            Console.WriteLine($"✅ Car güncellendi - Human Readable ID: {humanReadableId}");
                        }
                        else
                        {
                            Console.WriteLine($"⚠️ Car güncellenemedi - Human Readable ID: {humanReadableId}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("⚠️ JSON'da 'human_readable_id' alanı bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Car güncelleme hatası: {ex.Message}");
            }
        }
    }
}
