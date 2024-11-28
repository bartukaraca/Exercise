
using Microsoft.EntityFrameworkCore;
using Exercise.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Exercise.Application.Repositories.CarRepositories;
using Exercise.Persistence.Repositories.CarRepositories;
using Exercise.Application.Repositories.RoadRepositories;
using Exercise.Persistence.Repositories.RoadRepositories;
using Exercise.Application.Repositories.CarBrandRepositories;
using Exercise.Persistence.Repositories.CarBrandRepositories;
using Exercise.Domain.Entities.Identity;

namespace Exercise.Persistence
{
	public static class ServiceRegistration
	{
		public static void AddPersistenceServices(this IServiceCollection services)
		{
			services.AddDbContext<ExerciseDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
			services.AddIdentity<AppUser, AppRole>(options =>
			{
				options.Password.RequiredLength = 3;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
			})
			.AddEntityFrameworkStores<ExerciseDbContext>();

			services.AddScoped<ICarReadRepository, CarReadRepository>();
			services.AddScoped<ICarWriteRepository, CarWriteRepository>();
			services.AddScoped<IRoadReadRepository, RoadReadRepository>();
			services.AddScoped<IRoadWriteRepository, RoadWriteRepository>();
			services.AddScoped<ICarBrandReadRepository, CarBrandReadRepository>();
			services.AddScoped<ICarBrandWriteRepository, CarBrandWriteRepository>();

		}
	}
}
