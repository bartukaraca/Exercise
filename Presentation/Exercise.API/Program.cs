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

            // CORS yapılandırması - burada hangi kaynaklara izin verileceği belirtilmeli
            builder.Services.AddCors(options =>
                options.AddPolicy("AllowLocalhost", policy =>
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .WithOrigins("https://localhost:7085") // Frontend'inizin URL'si
                          .AllowCredentials() // Gerekirse credentials (cookie, authorization header vb.) için izin verir
                )
            );

            // Diğer servisler ekleniyor
            builder.Services.AddPersistenceServices();
            builder.Services.AddControllers();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices();
            builder.Services.AddSignalRServices();
            builder.Services.AddScoped<IRoadService, RoadService>();
            builder.Services.AddScoped<ICarService, CarService>();

            builder.Services.AddSingleton<JsonFileWatcherService>();
            builder.Services.AddHostedService<WatcherHostedService>();

            // Swagger yapılandırması
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // JWT Bearer Authentication yapılandırması
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Admin", options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = builder.Configuration["Token:Audience"],
                        ValidIssuer = builder.Configuration["Token:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                        LifetimeValidator = (notBefore, expires, securityTokent, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
                    };
                });

            var app = builder.Build();

            // JSON dosyası izleme servisini başlatma
            var fileWatcher = app.Services.GetRequiredService<JsonFileWatcherService>();
            fileWatcher.StartWatching();

            // HTTP pipeline yapılandırması
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // CORS, HTTPS yönlendirme, authentication ve authorization ekleniyor
            app.UseCors("AllowLocalhost"); // Burada "AllowLocalhost" politikası kullanılıyor
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers(); // API controller'larını haritalandır
            app.MapHubs(); // SignalR hub'larını haritalandır
            app.Run();
        }
    }
}
