using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using Polly;

namespace Ordering.Infrastructure.Data
{
    public class OrderingContextSeed
    {
        protected OrderingContextSeed()
        {
        }

        public static async Task SeedAsync(OrderingContext orderContext, ILogger<OrderingContextSeed> logger)
        {
            var policy = Policy.Handle<DbUpdateException>()
                .WaitAndRetryAsync(
                    retryCount: 5,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogError(
                            "[OrderContextSeed] Exception {message} with attempt {retry} of {policyKey}", exception.Message, retry, ctx.PolicyKey);
                    }
                );

            await policy.ExecuteAsync(async () =>
            {
                if (!await orderContext.Orders.AnyAsync())
                {
                    await orderContext.Orders.AddRangeAsync(GetPreconfiguredOrders());

                    await orderContext.SaveChangesAsync();

                    logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderingContext).Name);
                }
            });
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return
            [
                new Order()
                {
                    UserName = "adonis",
                    FirstName = "Minh",
                    LastName = "Vương",
                    Email = "hardingadonis@gmail.com",
                    AddressLine = "Quy Nhơn",
                    Country = "Vietnam",
                    TotalPrice = 350,
                    ZipCode = "123456",

                    CardName = "MasterCard",
                    CardNumber = "1234567890",
                    Expiration = "12/24",
                    CVV = "123",
                    PaymentMethod = PaymentMethod.CreditCard
                }
            ];
        }
    }
}