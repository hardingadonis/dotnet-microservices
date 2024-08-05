using Basket.Domain.Exceptions;

namespace Basket.Application.Exceptions
{
    public class DeleteBasketException : BasketException
    {
        public string UserName { get; set; }

        public DeleteBasketException(string userName)
            : base($"An error occurred while deleting the basket for the user: '{userName}'")
        {
            UserName = userName;
        }
    }
}
