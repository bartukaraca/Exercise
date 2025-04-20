using Exercise.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Domain.Entities
{
	public class Road :BaseEntitiy
	{
		public int RoadStatusId { get; set; }
        [ForeignKey(nameof(RoadStatusId))]

        public RoadStatus RoadStatus { get; set; }


	}
}
