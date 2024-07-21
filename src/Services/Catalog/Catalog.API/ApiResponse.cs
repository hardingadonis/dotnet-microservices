using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Catalog.API
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }

        public T? Data { get; set; }

        public string Message { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;

        public ModelStateDictionary? Error { get; set; }
    }
}
