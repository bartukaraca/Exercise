using Exercise.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Domain.Entities
{
	public class Car : BaseEntitiy
	{       
        public Guid CarBrandId { get; set; }
        public string Color { get; set; }
        public string VehicleIdentitiyNumber { get; set; }
        public string EngineType { get; set; }
        public string Transmission { get; set; }    


    }
}
