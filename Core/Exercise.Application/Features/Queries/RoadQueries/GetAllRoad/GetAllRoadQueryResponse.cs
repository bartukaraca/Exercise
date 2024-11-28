using Exercise.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Queries.RoadQueries.GetAllRoad
{
	public class GetAllRoadQueryResponse
	{
		public string Message { get; set; }
		public List<Road> Road { get; set; }
	}
}
