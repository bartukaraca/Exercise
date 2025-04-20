using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Abstractions.Hubs
{
	public interface ICarHubService
	{
		Task RoadStatusChangeMessageAsync(string message);
	}
}
