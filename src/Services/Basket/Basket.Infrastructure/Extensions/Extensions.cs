using Basket.Domain.Repositories;
using Basket.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Basket.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["DatabaseSettings:ConnectionString"]
                                        ?? throw new InvalidDataException("The Redis connection string is missing in the configuration. Please provide a valid connection string.");
            });

            services.AddHealthChecks()
                .AddRedis(
                    configuration["DatabaseSettings:ConnectionString"]
                        ?? throw new InvalidDataException("The Redis connection string is missing in the configuration. Please provide a valid connection string."),
                    name: "Basket Redis Health Check",
                    HealthStatus.Degraded
                );

            services.AddScoped<IBasketRepository, BasketRepository>();

            return services;
        }
    }
}
