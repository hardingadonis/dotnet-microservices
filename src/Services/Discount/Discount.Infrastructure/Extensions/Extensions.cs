using Discount.Domain.Repositories;
using Discount.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static async Task<IServiceCollection> AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IDiscountRepository, DiscountRepository>();

            var serviceProvider = services.BuildServiceProvider();
            await serviceProvider.MigrationDatabase<object>();

            return services;
        }
    }
}
