using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Commands.RoadCommands.CreateRoad
{
	public class CreateRoadCommandRequest : IRequest<CreateRoadCommandResponse>
	{
		public string Status { get; set; }
	}
}
