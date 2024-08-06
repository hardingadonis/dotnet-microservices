using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Infrastructure.Extensions
{
    public static class DiscountDbExtension
    {
        public static async Task<IServiceProvider> MigrationDatabase<TContext>(this IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();

            try
            {
                logger.LogInformation("Migrating database associated Started");

                await ApplyMigration(config);

                logger.LogInformation("Migrated database associated Completed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database.");

                throw new Exception("An error occurred while migrating the database.", ex);
            }

            return serviceProvider;
        }

        private static async Task ApplyMigration(IConfiguration config)
        {
            using var connection = new NpgsqlConnection(config["DatabaseSettings:ConnectionString"]);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();

            command.CommandText = "DROP TABLE IF EXISTS Coupon";
            command.ExecuteNonQuery();

            command.CommandText = @"
                CREATE TABLE Coupon(
                    Id SERIAL PRIMARY KEY,
                    ProductName VARCHAR(255) NOT NULL,
                    Description TEXT,
                    Amount INT
                )";
            command.ExecuteNonQuery();

            command.CommandText = @"
                INSERT INTO Coupon(ProductName, Description, Amount)
                VALUES  ('Adidas Quick Force Indoor Badminton Shoes', 'Shoes Discount', 150),
                        ('Adidas FIFA World Cup 2018 OMB Football (White/Red/Black)', 'Football Discount', 500)";
            command.ExecuteNonQuery();
        }
    }
}
