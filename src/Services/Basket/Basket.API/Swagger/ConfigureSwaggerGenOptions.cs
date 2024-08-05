namespace Basket.API.Swagger
{
    public class ConfigureSwaggerGenOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly IConfiguration _configuration;

        public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider, IConfiguration configuration)
        {
            _provider = provider;
            _configuration = configuration;
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
            var contactUrl = _configuration["Basket.API.Settings:ContactUrl"];
            var licenseUrl = _configuration["Basket.API.Settings:LicenseUrl"];

            var info = new OpenApiInfo()
            {
                Title = $"Basket.API v{description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Contact = new OpenApiContact()
                {
                    Name = "Minh Vương",
                    Email = "hardingadonis@gmail.com",
                    Url = new Uri(contactUrl!)
                },
                License = new OpenApiLicense()
                {
                    Name = "MIT License",
                    Url = new Uri(licenseUrl!)
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