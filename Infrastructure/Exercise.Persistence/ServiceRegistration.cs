
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
using Exercise.Domain.Entities.Identity;
using Exercise.Application.Abstractions.Services;
using Exercise.Persistence.Services;
using Exercise.Application.Abstractions.Services.Authentications;
using ETicaretAPI.Persistence.Services;
using Exercise.Application.Services;

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

			services.AddScoped<IDataService, DataService>();
			services.AddScoped<IMainService, MainService>();


			services.AddScoped<ICarReadRepository, CarReadRepository>();
			services.AddScoped<ICarWriteRepository, CarWriteRepository>();
			services.AddScoped<IRoadReadRepository, RoadReadRepository>();
			services.AddScoped<IRoadWriteRepository, RoadWriteRepository>();
	

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IInternalAuthentication, AuthService>();
		}
	}
}
