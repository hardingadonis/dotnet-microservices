using AutoMapper;

namespace Catalog.Application.Mappers
{
    public static class CatalogMapper
    {
        private static readonly Lazy<IMapper> _lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod?.IsPublic == true || p.GetMethod?.IsAssembly == true;

                cfg.AddProfile<CatalogMappingProfile>();
            });

            return config.CreateMapper();
        });

        public static IMapper Mapper => _lazy.Value;
    }
}
