using System.Net.Http;
using System.Net.Http.Json;
using Exercise.WebUI.Models; // DTO klasörünü buna göre ayarla
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exercise.WebUI.Services
{
    public class ExerciseService
    {
        private readonly HttpClient _httpClient;

        public ExerciseService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ExerciseApi");
        }

        public async Task<List<ExerciseDto>> GetAllExercisesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ExerciseDto>>("exercises");
            return response ?? new List<ExerciseDto>();
        }
    }
}
