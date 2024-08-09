namespace Ordering.Domain.Entities
{
    public enum PaymentMethod : byte
    {
        Cash = 1,
        CreditCard = 2,
        DebitCard = 3
    }

    public class Order : BaseEntity
    {
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

        public PaymentMethod PaymentMethod { get; set; }
    }
}