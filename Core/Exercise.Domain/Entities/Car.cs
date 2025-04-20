using Exercise.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Domain.Entities
{
	public class Car : BaseEntitiy
	{
		public string VehicleNumber { get; set; }
		public int RoadId { get; set; }


		[ForeignKey(nameof(RoadId))]
		public Road Road { get; set; }

    }
}
