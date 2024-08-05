namespace Basket.Domain.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; } = string.Empty;

        public IList<ShoppingCartItem> Items { get; set; } = [];
    }
}
