namespace Ordering.Application.Responses
{
    public class OrderResponse
    {
        public long Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string AddressLine { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string ZipCode { get; set; } = string.Empty;

        public string CardName { get; set; } = string.Empty;

        public string CardNumber { get; set; } = string.Empty;

        public string Expiration { get; set; } = string.Empty;

        public string CVV { get; set; } = string.Empty;

        public int PaymentMethod { get; set; }
    }
}