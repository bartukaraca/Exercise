using Exercise.Application.Abstractions.Hubs;
using Exercise.Application.Repositories.RoadRepositories;
using Exercise.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Commands.RoadCommands.UpdateRoad
{
	public class UpdateRoadCommandHandler : IRequestHandler<UpdateRoadCommandRequest, UpdateRoadCommandResponse>
	{
		readonly IRoadReadRepository _roadReadRepository;
		readonly IRoadWriteRepository _roadWriteRepository;
		readonly ICarHubService _carHubService;

		public UpdateRoadCommandHandler(IRoadReadRepository roadReadRepository, IRoadWriteRepository roadWriteRepository, ICarHubService carHubService)
		{
			_roadReadRepository = roadReadRepository;
			_roadWriteRepository = roadWriteRepository;
			_carHubService = carHubService;
		}

		public async Task<UpdateRoadCommandResponse> Handle(UpdateRoadCommandRequest request, CancellationToken cancellationToken)
		{
			Road road = await _roadReadRepository.GetByIdAsync(request.Id);
			road.RoadStatusId = request.RoadStatusId;
			await _roadWriteRepository.SaveAsync();
			await _carHubService.RoadStatusChangeMessageAsync($"{request.RoadStatusId}");

			return new UpdateRoadCommandResponse
			{
				Message = "Yol Durumu Başarıyla Güncellendi",
				Success = true,
			};
		}
	}
}
