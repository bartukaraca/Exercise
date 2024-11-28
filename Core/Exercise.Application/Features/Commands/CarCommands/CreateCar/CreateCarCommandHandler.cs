﻿using Exercise.Application.Repositories.CarRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Commands.CarCommands.CreateCar
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommandRequest, CreateCarCommandResponse>
    {
        readonly ICarWriteRepository _carWriteRepository;

        public CreateCarCommandHandler(ICarWriteRepository carWriteRepository)
        {
            _carWriteRepository = carWriteRepository;
        }

        public async Task<CreateCarCommandResponse> Handle(CreateCarCommandRequest request, CancellationToken cancellationToken)
        {
            await _carWriteRepository.AddAsync(new()
            {
                EngineType = request.EngineType,
                Color = request.Color,
                Transmission = request.Transmission,
                VehicleIdentitiyNumber = request.VehicleIdentitiyNumber,
                CarBrandId = request.CarBrandId,
            });
            await _carWriteRepository.SaveAsync();
            return new CreateCarCommandResponse
            {
                Success = true,
                Message = "Araç Başarıyla Oluşturuldu."
            };
        }
    }
}
