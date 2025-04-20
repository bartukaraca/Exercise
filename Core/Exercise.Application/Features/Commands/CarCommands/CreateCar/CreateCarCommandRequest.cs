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
        
        public int RoadId { get; set; }
        public string VehicleIdentitiyNumber { get; set; }
       
    }
}
