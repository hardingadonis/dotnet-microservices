using Catalog.Domain.Exceptions;

namespace Catalog.Application.Exceptions
{
    public class InvalidIdException : CatalogException
    {
        public string Id { get; set; }

        public InvalidIdException(string id)
            : base($"The Id '{id}' is not a valid 24-character hexadecimal string.")
        {
            Id = id;
        }
    }
}
