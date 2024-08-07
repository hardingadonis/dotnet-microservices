using Discount.Domain.Entities;

namespace Discount.Domain.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon?> GetDiscount(string productName);

        Task<Coupon?> CreateDiscount(Coupon coupon);

        Task<bool> UpdateDiscount(Coupon coupon);

        Task<bool> DeleteDiscount(string productName);
    }
}