using Exercise.Application.Repositories;
using Exercise.Application.Repositories.RoadRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Queries.RoadQueries.GetAllRoad
{
	public class GetAllRoadQueryHandler : IRequestHandler<GetAllRoadQueryRequest, GetAllRoadQueryResponse>
	{
		readonly IRoadReadRepository _roadReadRepository;

		public GetAllRoadQueryHandler(IRoadReadRepository roadReadRepository)
		{
			_roadReadRepository = roadReadRepository;
		}

		public async Task<GetAllRoadQueryResponse> Handle(GetAllRoadQueryRequest request, CancellationToken cancellationToken)
		{
			var roads = _roadReadRepository.GetAll().Include(r=>r.RoadStatus).ToList();
			return new()
			{
				Message = "Yollar Başarıyla Getirildi.",
				Road = roads
			};
		}
	}
}
