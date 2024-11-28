using Exercise.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Commands.CarCommands.CreateCar
{
    public class CreateCarCommandRequest : IRequest<CreateCarCommandResponse>
    {
        public Guid CarBrandId { get; set; }
        public string Color { get; set; }
        public string VehicleIdentitiyNumber { get; set; }
        public string EngineType { get; set; }
        public string Transmission { get; set; }
    }
}
