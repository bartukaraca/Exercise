using Exercise.Application.Repositories.RoadRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Commands.RoadCommands.RemoveRoad
{
	public class RemoveRoadCommandHandler : IRequestHandler<RemoveRoadCommandRequest, RemoveRoadCommandResponse>
	{
		readonly IRoadWriteRepository _roadWriteRepository;

		public RemoveRoadCommandHandler(IRoadWriteRepository roadWriteRepository)
		{
			_roadWriteRepository = roadWriteRepository;
		}

		public async Task<RemoveRoadCommandResponse> Handle(RemoveRoadCommandRequest request, CancellationToken cancellationToken)
		{
			await _roadWriteRepository.RemoveAsync(request.Id);
			await _roadWriteRepository.SaveAsync();
			return new RemoveRoadCommandResponse
			{
				Message = "Yol Başarıyla Silindi.",
				Success = true,
			};
		}
	}
}
