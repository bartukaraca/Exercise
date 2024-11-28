using Exercise.Application.Repositories.RoadRepositories;
using Exercise.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Queries.RoadQueries.GetRoadById
{
	public class GetRoadByIdQueryHandler : IRequestHandler<GetRoadByIdQueryRequest, GetRoadByIdQueryResponse>
	{
		readonly IRoadReadRepository _roadReadRepository;

		public GetRoadByIdQueryHandler(IRoadReadRepository roadReadRepository)
		{
			_roadReadRepository = roadReadRepository;
		}

		public async Task<GetRoadByIdQueryResponse> Handle(GetRoadByIdQueryRequest request, CancellationToken cancellationToken)
		{
			Road road = await _roadReadRepository.GetByIdAsync(request.Id,false);
			return new()
			{
				Message = "Yol Başarıyla Getirildi",
				Road = road
			};
		}
	}
}
