using Exercise.Application.Abstractions.Hubs;
using Exercise.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.SignalR
{
	public static class ServiceRegistration
	{
		public static void AddSignalRServices(this IServiceCollection collection)
		{
			collection.AddTransient<ICarHubService, CarHubService>();
			collection.AddSignalR();
		}
	}
}
