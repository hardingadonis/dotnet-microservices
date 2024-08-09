using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Catalog.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddMongoDb(
                    configuration["DatabaseSettings:ConnectionString"]
                        ?? throw new InvalidDataException("The MongoDB connection string is missing in the configuration. Please provide a valid connection string."),
                    name: "Catalog MongDB Health Check",
                    HealthStatus.Degraded
                );

            services.AddScoped<ICatalogContext, CatalogContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}
