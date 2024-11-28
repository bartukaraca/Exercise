using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Commands.CarBrandCommands.CreateCarBrand
{
	public class CreateCarBrandCommandRequest :IRequest<CreateCarBrandCommandResponse>
	{
		public string Name { get; set; }
		public string Country { get; set; }


	}
}
