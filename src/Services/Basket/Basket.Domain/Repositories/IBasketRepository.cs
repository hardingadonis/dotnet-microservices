using Basket.Domain.Entities;

namespace Basket.Domain.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart?> GetBasket(string userName);

        Task<ShoppingCart?> UpdateBasket(ShoppingCart basket);

        Task<bool> DeleteBasket(string userName);
    }
}
