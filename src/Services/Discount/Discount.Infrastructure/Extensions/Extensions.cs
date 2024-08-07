using Discount.Domain.Repositories;
using Discount.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Discount.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static async Task<IServiceCollection> AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDiscountRepository, DiscountRepository>();

            var serviceProvider = services.BuildServiceProvider();
            await serviceProvider.MigrationDatabase<object>();

            services.AddHealthChecks()
                .AddNpgSql(
                    configuration["DatabaseSettings:ConnectionString"]
                        ?? throw new InvalidDataException("The PostgreSQL connection string is missing in the configuration. Please provide a valid connection string."),
                    name: "Discount PostgreSQL Health Check",
                    failureStatus: HealthStatus.Degraded
                );

            return services;
        }
    }
}
