using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using AppServer.DAL.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AppServer.BLL.Calculations;
using Hangfire;

namespace AppServer.BLL.Extensions.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<AppServerDbContext>();

            services.AddCors(options =>
            {
                var front_url = configuration.GetValue<string>("front_url");
                var webserver = configuration.GetValue<string>("web_server");
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(front_url).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                    policy.WithOrigins(webserver).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });

            var connection_string = configuration.GetConnectionString("connection_string");
            //var connection_string = configuration.GetConnectionString("docker_connection_string");
            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connection_string));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddScoped<DataCalculation>();
        }
    }
}
