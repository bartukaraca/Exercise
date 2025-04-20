using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Commands.RoadCommands.UpdateRoad
{
	public class UpdateRoadCommandRequest : IRequest<UpdateRoadCommandResponse>
	{
        public string Id { get; set; }
        public int RoadStatusId { get; set; }   
    }
}
