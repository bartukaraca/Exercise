using System.Text.Json.Serialization;

public class CarDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("vehicleNumber")]
    public string VehicleNumber { get; set; }

    [JsonPropertyName("roadId")]
    public int RoadId { get; set; }
}
