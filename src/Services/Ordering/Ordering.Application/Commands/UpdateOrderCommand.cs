using MediatR;

namespace Ordering.Application.Commands
{
    public record UpdateOrderCommand(
        long Id,
        string UserName,
        decimal TotalPrice,
        string FirstName,
        string LastName,
        string Email,
        string AddressLine,
        string Country,
        string State,
        string ZipCode,
        string CardName,
        string CardNumber,
        string Expiration,
        string CVV,
        int PaymentMethod
    ) : IRequest<Unit>;
}