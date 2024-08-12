using Catalog.Domain.Exceptions;
using FluentValidation.Results;

namespace Catalog.Application.Exceptions
{
    public class GroupedValidationException : CatalogException
    {
        public IDictionary<string, string[]> Errors { get; }

        public GroupedValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public GroupedValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
    }
}