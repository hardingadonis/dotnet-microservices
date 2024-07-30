namespace Basket.Application.Responses
{
    public class BasketResponse
    {
        public string UserName { get; set; } = string.Empty;

        public List<BasketItemResponse> Items { get; set; } = [];

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;

                foreach (var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }

                return totalPrice;
            }
        }
    }
}