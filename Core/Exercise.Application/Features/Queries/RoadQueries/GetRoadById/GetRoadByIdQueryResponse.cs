using Exercise.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Queries.RoadQueries.GetRoadById
{
	public class GetRoadByIdQueryResponse
	{
        public string Message { get; set; }
        public Road Road { get; set; }
    }
}
