using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Queries.RoadQueries.GetRoadById
{
	public class GetRoadByIdQueryRequest :IRequest<GetRoadByIdQueryResponse>
	{
        public int Id { get; set; }	
    }
}
