using Dapper;
using Discount.Domain.Entities;
using Discount.Domain.Repositories;
using Discount.Infrastructure.Queries;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly string _connectionString;

        public DiscountRepository(IConfiguration configuration)
        {
            _connectionString = configuration["DatabaseSettings:ConnectionString"]
                ?? throw new InvalidDataException("The PostgreSQL connection string is missing in the configuration. Please provide a valid connection string.");
        }

        private static async Task<Coupon?> IsExistingCoupon(string productName, NpgsqlConnection connection)
        {
            return await connection.QueryFirstOrDefaultAsync<Coupon?>(CouponSqlQueries.GetCoupon, new { ProductName = productName });
        }

        public async Task<Coupon?> CreateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_connectionString);

            return await connection.QueryFirstOrDefaultAsync<Coupon>(CouponSqlQueries.CreateCoupon, coupon);
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_connectionString);

            var deletedCoupon = await connection.ExecuteAsync(CouponSqlQueries.DeleteCoupon, new { ProductName = productName });

            return deletedCoupon > 0;
        }

        public async Task<Coupon?> GetDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_connectionString);

            return await IsExistingCoupon(productName, connection);
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_connectionString);

            var existingCoupon = await IsExistingCoupon(coupon.ProductName, connection);

            if (existingCoupon is null)
            {
                return false;
            }

            coupon.Id = existingCoupon.Id;

            var updatedCoupon = await connection.ExecuteAsync(CouponSqlQueries.UpdateCoupon, coupon);

            return updatedCoupon > 0;
        }
    }
}
