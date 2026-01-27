using DeepAzureServer.Data;
using DeepAzureServer.Services.Implementations;
using DeepAzureServer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeepAzureServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<AppDbContext>(options =>
                           options.UseNpgsql(
                               builder.Configuration.GetConnectionString("DefaultConnection"),
                               npgsqlOptions => npgsqlOptions.MigrationsAssembly("DeepAzureServer")
                           ));
            builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!);

            // Scoped DI
            builder.Services.AddScoped<IAuthService, AuthService>();

            var app = builder.Build();
            app.MapHealthChecks("/health");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
