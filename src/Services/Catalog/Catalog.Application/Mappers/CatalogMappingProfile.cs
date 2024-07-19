using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Domain.Entities;

namespace Catalog.Application.Mappers
{
    public class CatalogMappingProfile : Profile
    {
        public CatalogMappingProfile()
        {
            CreateMap<Brand, BrandResponse>().ReverseMap();
        }
    }
}
