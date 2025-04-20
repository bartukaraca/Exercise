
using Exercise.Application;
using Exercise.Persistence;
using Exercise.Infrastructure;
using Exercise.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Exercise.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Exercise.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Exercise.SignalR.Hubs;
using Exercise.Application.Abstractions.Services;
using Exercise.Persistence.Services;



namespace Exercise.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
            policy.AllowAnyHeader().AllowAnyMethod().WithOrigins()));

            builder.Services.AddPersistenceServices();
            builder.Services.AddControllers();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices();
            builder.Services.AddSignalRServices();
            builder.Services.AddScoped<IRoadService, RoadService>();
            builder.Services.AddSingleton<JsonFileWatcherService>();
            builder.Services.AddHostedService<WatcherHostedService>();



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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                        LifetimeValidator = (notBefore, expires, securityTokent, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
                    };
                });
            var app = builder.Build();

            var fileWatcher = app.Services.GetRequiredService<JsonFileWatcherService>();
            fileWatcher.StartWatching();

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
            app.MapHubs();
            app.Run();
        }
    }
}
