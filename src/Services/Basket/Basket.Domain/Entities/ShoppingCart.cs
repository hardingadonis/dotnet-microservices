namespace Basket.Domain.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; } = string.Empty;

        public List<ShoppingCartItem> Items { get; set; } = [];
    }
}
