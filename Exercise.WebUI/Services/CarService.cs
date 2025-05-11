using System.Net.Http;
using System.Net.Http.Json;
using Exercise.WebUI.Models; // DTO klasörünü bu namespace'e göre ayarlamalısın
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;

namespace Exercise.WebUI.Services
{
    public class CarService
    {
        private readonly HttpClient _httpClient;

        public CarService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ExerciseApi");
        }

        public async Task<List<CarDTO>> GetAllCarsAsync()
        {
            var response = await _httpClient.GetAsync("Cars"); // 'cars' → 'Cars' büyük/küçük harfe duyarlı olabilir!
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(jsonString);
            var root = document.RootElement;

            // Eğer API cevabı şu formatta ise: { "car": [...] }
            var carArray = root.GetProperty("car");

            var cars = new List<CarDTO>();
            foreach (var carElement in carArray.EnumerateArray())
            {
                var car = new CarDTO
                {
                    Id = carElement.GetProperty("id").GetInt32(),
                    VehicleNumber = carElement.GetProperty("vehicleNumber").GetString(),
                    RoadId = carElement.GetProperty("roadId").GetInt32()
                };
                cars.Add(car);
            }

            return cars;
        }

        public async Task<bool> DeleteCarAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Cars/{id}");
            if (!response.IsSuccessStatusCode)
                return false;

            var jsonString = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(jsonString);
            var root = document.RootElement;

            return root.GetProperty("success").GetBoolean();
        }

    }
}
