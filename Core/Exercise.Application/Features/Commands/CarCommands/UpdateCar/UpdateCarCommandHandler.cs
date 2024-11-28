using Exercise.Application.Repositories;
using Exercise.Application.Repositories.CarRepositories;
using Exercise.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Commands.CarCommands.UpdateCar
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommandRequest, UpdateCarCommandResponse>
    {
        readonly ICarReadRepository _carReadRepository;
        readonly ICarWriteRepository _carWriteRepository;

        public UpdateCarCommandHandler(ICarWriteRepository carWriteRepository, ICarReadRepository carReadRepository)
        {
            _carWriteRepository = carWriteRepository;
            _carReadRepository = carReadRepository;
        }

        public async Task<UpdateCarCommandResponse> Handle(UpdateCarCommandRequest request, CancellationToken cancellationToken)
        {
            Car car = await _carReadRepository.GetByIdAsync(request.Id);
            car.Transmission = request.Transmission;
            car.Color = request.Color;
            car.CarBrandId = request.CarBrandId;
            car.EngineType = request.EngineType;
            car.VehicleIdentitiyNumber = request.VehicleIdentitiyNumber;
            await _carWriteRepository.SaveAsync();
            return new UpdateCarCommandResponse
            {
                Message = "Araç Başarıyla Güncellendi",
                Success = true
            };
        }
    }
}
