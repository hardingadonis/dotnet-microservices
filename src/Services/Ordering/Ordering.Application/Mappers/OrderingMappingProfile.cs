using AutoMapper;
using Ordering.Application.Responses;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappers
{
    public class OrderingMappingProfile : Profile
    {
        public OrderingMappingProfile()
        {
            CreateMap<Order, OrderResponse>().ReverseMap();
        }
    }
}