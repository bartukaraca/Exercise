using System;

namespace Exercise.Application.DTOs
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string VehicleNumber { get; set; } // human_readable_id saklanabilir
        public int RoadId { get; set; }

        // Opsiyonel: Eğer vehicle_id'yi de saklamak isterseniz
        public string VehicleId { get; set; } // vehicle_id saklanabilir

        // Opsiyonel: Diğer bilgiler
        public DateTime? LastDetectionTime { get; set; }
    }
}