using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Commands.CarCommands.UpdateCar
{
    public class UpdateCarCommandRequest : IRequest<UpdateCarCommandResponse>
    {
        public string Id { get; set; }
        public int RoadId { get; set; }
        public string VehicleIdentitiyNumber { get; set; }
       
    }
}
