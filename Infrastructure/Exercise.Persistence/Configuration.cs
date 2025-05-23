﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Persistence
{
	static class Configuration
	{
		static public string ConnectionString
		{
			get
			{
				ConfigurationManager configurationManager = new();
				configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/Exercise.API"));
				configurationManager.AddJsonFile("appsettings.json");
				return configurationManager.GetConnectionString("PostgreSQL");
			}
		}
		static public string ExternalConnectionString
		{
			get
			{
				ConfigurationManager configurationManager = new();
				configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/Exercise.API"));
				configurationManager.AddJsonFile("appsettings.json");
				return configurationManager.GetConnectionString("ExternalDatabase");
			}
		}
	}
}
