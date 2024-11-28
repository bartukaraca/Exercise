using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Commands.RoadCommands.RemoveRoad
{
	public class RemoveRoadCommandRequest :IRequest<RemoveRoadCommandResponse>
	{
        public string Id { get; set; }	
    }
}
