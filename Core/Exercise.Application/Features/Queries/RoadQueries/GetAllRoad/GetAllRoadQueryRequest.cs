using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Queries.RoadQueries.GetAllRoad
{
	public class GetAllRoadQueryRequest :IRequest<GetAllRoadQueryResponse>
	{
	}
}
