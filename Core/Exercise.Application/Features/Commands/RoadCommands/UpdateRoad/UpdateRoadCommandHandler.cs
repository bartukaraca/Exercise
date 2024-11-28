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

		public UpdateRoadCommandHandler(IRoadReadRepository roadReadRepository, IRoadWriteRepository roadWriteRepository)
		{
			_roadReadRepository = roadReadRepository;
			_roadWriteRepository = roadWriteRepository;
		}

		public async Task<UpdateRoadCommandResponse> Handle(UpdateRoadCommandRequest request, CancellationToken cancellationToken)
		{
			Road road = await _roadReadRepository.GetByIdAsync(request.Id);
			road.Status = request.Status;
			await _roadWriteRepository.SaveAsync();
			return new UpdateRoadCommandResponse
			{
				Message = "Yol Durumu Başarıyla Güncellendi",
				Success = true,
			};
		}
	}
}
