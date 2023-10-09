using AppServer.Data;

namespace AppServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<HeavyTaskDbContext>();
            services.AddCors(options =>
            {
                var front_url = configuration.GetValue<string>("front_url");
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(front_url).AllowAnyMethod().AllowAnyHeader();
                });
            });
        }
    }
}
