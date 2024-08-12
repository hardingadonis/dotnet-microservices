using Basket.Application.Behaviours;
using Basket.Application.GrpcServices;
using Discount.Grpc.Protos;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Basket.Application.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Extensions).Assembly));
            services.AddAutoMapper(typeof(Extensions).Assembly);

            services.AddScoped<DiscountGrpcService>();
            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
            {
                string discountUrl = configuration["GrpcSettings:DiscountUrl"]
                    ?? throw new InvalidDataException("Invalid or missing 'GrpcConfigs:DiscountUrl' configuration setting.");

                options.Address = new Uri(discountUrl);
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));

            return services;
        }
    }
}
