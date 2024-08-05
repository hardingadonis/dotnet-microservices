using AutoMapper;

namespace Basket.Application.Mappers
{
    public static class BasketMapper
    {
        private readonly static Lazy<IMapper> _lazy = new Lazy<IMapper>(() =>
        {
            var cfg = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod?.IsPublic == true || p.GetMethod?.IsAssembly == true;

                cfg.AddProfile<BasketMappingProfile>();
            });

            return cfg.CreateMapper();
        });

        public static IMapper Mapper => _lazy.Value;
    }
}
