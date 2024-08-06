namespace Discount.Infrastructure.Queries
{
    public static class CouponSqlQueries
    {
        public const string GetCoupon = @"
            SELECT *
            FROM Coupon
            WHERE ProductName = @ProductName";

        public const string CreateCoupon = @"
            INSERT INTO Coupon (ProductName, Description, Amount)
            VALUES (@ProductName, @Description, @Amount)
            RETURNING *";

        public const string UpdateCoupon = @"
            UPDATE Coupon
            SET ProductName = @ProductName, Description = @Description, Amount = @Amount
            WHERE Id = @Id";

        public const string DeleteCoupon = @"
            DELETE FROM Coupon
            WHERE ProductName = @ProductName";
    }
}
