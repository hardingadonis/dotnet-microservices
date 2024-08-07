using Microsoft.Extensions.DependencyInjection;

namespace Discount.Application.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Extensions).Assembly));
            services.AddAutoMapper(typeof(Extensions).Assembly);
            services.AddGrpc();

            return services;
        }
    }
}