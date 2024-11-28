
using Exercise.Application;
using Exercise.Persistence;
using Exercise.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace Exercise.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddPersistenceServices();
			builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
			policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
			builder.Services.AddControllers();
			builder.Services.AddApplicationServices();
			builder.Services.AddInfrastructureServices();


			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer("Admin", options =>
				{
					options.TokenValidationParameters = new()
					{
						ValidateAudience = true, //oluþacak token deðerini kimlerin(sitelerin) kullanacaðýný belirleriz. www."".com gibi
						ValidateIssuer = true, //oluþacak token deðerini kimin daðýttýðýný ifade eder. www.myapi.com gibi
						ValidateLifetime = true,//oluþturulan token deðerinin süresini kontrol eder.
						ValidateIssuerSigningKey = true, // oluþacak token deðerinin uygulamaya ait deðerini ifade eder.

						ValidAudience = builder.Configuration["Token:Audience"],
						ValidIssuer = builder.Configuration["Token:Issuer"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
					};
				});
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseCors();
			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
