namespace Exercise.WebUI.Models
{
    public class RoadStatusDto
    {
        public string Status { get; set; }
    }

    public class RoadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoadStatusDto RoadStatus { get; set; } 
    }

    public class GetAllRoadResponse
    {
        public string Message { get; set; }
        public List<RoadDto> Road { get; set; } = new();
    }
}
