using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Repositories;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("OrderingConnectionString");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidDataException("The 'OrderingConnectionString' configuration setting is missing or empty.");
            }

            services.AddDbContext<OrderingContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null
                    );
                });
            });

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }

        public static async Task SeedDataAsync(this IServiceProvider serviceProvier)
        {
            using var scope = serviceProvier.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var logger = scopedServices.GetRequiredService<ILogger<OrderingContextSeed>>();

            try
            {
                var context = scopedServices.GetRequiredService<OrderingContext>();
                await OrderingContextSeed.SeedAsync(context, logger);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating or seeding the database.");
            }
        }
    }
}