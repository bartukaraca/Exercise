using Exercise.Application.Abstractions.Hubs;
using Exercise.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.SignalR.HubServices
{
	public class CarHubService : ICarHubService
	{
		readonly IHubContext<CarHub> _hubContext;

		public CarHubService(IHubContext<CarHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public async Task RoadStatusChangeMessageAsync(string message)
		{
			await _hubContext.Clients.All.SendAsync(ReceiveFuctionNames.RoadStatusChangeMessage, message);
		}
	}
}
