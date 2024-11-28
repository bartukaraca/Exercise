using Exercise.Application.Repositories.CarBrandRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Commands.CarBrandCommands.CreateCarBrand
{
	public class CreateCarBrandCommandHandler : IRequestHandler<CreateCarBrandCommandRequest, CreateCarBrandCommandResponse>
	{
		readonly ICarBrandWriteRepository _carBrandWriteRepository;

		public CreateCarBrandCommandHandler(ICarBrandWriteRepository carBrandWriteRepository)
		{
			_carBrandWriteRepository = carBrandWriteRepository;
		}

		public async Task<CreateCarBrandCommandResponse> Handle(CreateCarBrandCommandRequest request, CancellationToken cancellationToken)
		{
			await _carBrandWriteRepository.AddAsync(new()
			{
				Country = request.Country,
				Name = request.Name,
			});
			await _carBrandWriteRepository.SaveAsync();
			return new CreateCarBrandCommandResponse
			{
				Message = "Marka Başarıyla Oluşturuldu.",
				Success = true,
			};
				
		}
	}
}
