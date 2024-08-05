using Basket.Domain.Exceptions;

namespace Basket.Application.Exceptions
{
    public class BasketNotFoundException : BasketException
    {
        public string UserName { get; set; }

        public BasketNotFoundException(string userName)
            : base($"No basket found for user: '{userName}'")
        {
            UserName = userName;
        }
    }
}
