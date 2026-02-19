using System.Text;
using DeepAzureServer.Data;
using DeepAzureServer.Data.Configurations;
using DeepAzureServer.Infrastructures;
using DeepAzureServer.Models.Entities;
using DeepAzureServer.Repositories.Implementations;
using DeepAzureServer.Repositories.Interfaces;
using DeepAzureServer.Services.Implementations;
using DeepAzureServer.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

namespace DeepAzureServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<JwtSettings>(
                builder.Configuration.GetSection("JwtSettings")
            );
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    npgsqlOptions => npgsqlOptions.MigrationsAssembly("DeepAzureServer")
                )
            );
            builder
                .Services.AddIdentity<User, IdentityRole<long>>(options =>
                {
                    options.User.RequireUniqueEmail = true;

                    if (builder.Environment.IsDevelopment())
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequiredLength = 3;
                    }
                    else
                    {
                        options.Password.RequireDigit = true;
                        options.Password.RequireNonAlphanumeric = true;
                        options.Password.RequiredLength = 12;
                    }
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            builder
                .Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var jwtSettings = builder
                        .Configuration.GetSection("JwtSettings")
                        .Get<JwtSettings>();
                    var key = Encoding.ASCII.GetBytes(jwtSettings!.SecretKey);

                    options.TokenValidationParameters =
                        new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = true,
                            ValidIssuer = jwtSettings.Issuer,
                            ValidateAudience = true,
                            ValidAudience = jwtSettings.Audience,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,
                        };

                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();

                            // Return a 401 Unauthorized + Error Message
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";
                            var result = System.Text.Json.JsonSerializer.Serialize(
                                new { error = "You are not authorized" }
                            );
                            return context.Response.WriteAsync(result);
                        },
                    };
                });
            builder.Services.AddControllers();
            builder.Services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer(
                    (document, context, cancellationToken) =>
                    {
                        document.Servers = new List<Microsoft.OpenApi.Models.OpenApiServer>
                        {
                            new Microsoft.OpenApi.Models.OpenApiServer
                            {
                                Url = "http://localhost:5000",
                            },
                        };
                        return Task.CompletedTask;
                    }
                );
            });
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
            builder
                .Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!);

            // --- REPO INJECT
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IMonsterRepository, MonsterRepository>();
            builder.Services.AddScoped<IElementRepository, ElementRepository>();
            builder.Services.AddScoped<IAbilityRepository, AbilityRepository>();
            // --- SERVICE INJECT ---
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IMonsterService, MonsterService>();
            builder.Services.AddScoped<IElementService, ElementService>();
            builder.Services.AddScoped<IAbilityService, AbilityService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAll",
                    policy =>
                    {
                        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    }
                );
            });

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    await DbInitializer.SeedData(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            app.UseExceptionHandler();
            app.MapHealthChecks("/health");
            app.UseCors("AllowAll");
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            await app.RunAsync();
        }
    }
}
