using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Catalog.API.Swagger
{
    public class ConfigureSwaggerGenOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = $"Catalog.API v{description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Contact = new OpenApiContact()
                {
                    Name = "Minh Vương",
                    Email = "hardingadonis@gmail.com",
                    Url = new Uri("https://hardingadonis.github.io")
                },
                License = new OpenApiLicense()
                {
                    Name = "MIT License",
                    Url = new Uri("https://github.com/hardingadonis/dotnet-microservices/blob/main/LICENSE")
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
