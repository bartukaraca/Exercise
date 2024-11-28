using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Commands.CarCommands.RemoveCar
{
    public class RemoveCarCommandRequest : IRequest<RemoveCarCommandResponse>
    {

        public string Id { get; set; }
    }
}
