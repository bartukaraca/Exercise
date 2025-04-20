using Exercise.Application.Repositories.RoadRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Commands.RoadCommands.CreateRoad
{
	public class CreateRoadCommandHandler : IRequestHandler<CreateRoadCommandRequest, CreateRoadCommandResponse>
	{
		readonly IRoadWriteRepository _roadWriteRepository;

		public CreateRoadCommandHandler(IRoadWriteRepository roadWriteRepository)
		{
			_roadWriteRepository = roadWriteRepository;
		}

		public async Task<CreateRoadCommandResponse> Handle(CreateRoadCommandRequest request, CancellationToken cancellationToken)
		{
			await _roadWriteRepository.AddAsync(new()
			{
			RoadStatusId=request.RoadStatusId,
			});
			await _roadWriteRepository.SaveAsync();
			return new CreateRoadCommandResponse
			{
				Message = "Yol Başarıyla Oluşturuldu",
				Success = true,
			};
		}
	}
}
