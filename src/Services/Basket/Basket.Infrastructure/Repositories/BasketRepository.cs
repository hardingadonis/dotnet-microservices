using Basket.Domain.Entities;
using Basket.Domain.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository(IDistributedCache redisCache) : IBasketRepository
    {
        private readonly IDistributedCache _redisCache = redisCache;

        public async Task<ShoppingCart?> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);

            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }

            return JsonSerializer.Deserialize<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart?> UpdateBasket(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket));

            return await GetBasket(basket.UserName);
        }

        public async Task<bool> DeleteBasket(string userName)
        {
            var basket = await GetBasket(userName);

            if (basket !=null)
            {
                await _redisCache.RemoveAsync(userName);

                return true;
            }

            return false;
        }
    }
}
