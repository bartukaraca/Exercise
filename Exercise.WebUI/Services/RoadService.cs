using Exercise.WebUI.Models;
using System.Text.Json;

public class RoadService
{
    private readonly HttpClient _httpClient;

    public RoadService(IHttpClientFactory httpClientFactory)
    {
      
        _httpClient = httpClientFactory.CreateClient("ExerciseApi");
    }

    public async Task<GetAllRoadResponse> GetAllRoadsAsync()
    {
        var response = await _httpClient.GetAsync("Roads"); 

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        var roadsFromApi = JsonSerializer.Deserialize<GetAllRoadResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        
        return roadsFromApi;
    }
}
