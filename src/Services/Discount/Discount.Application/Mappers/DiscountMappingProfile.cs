using AutoMapper;
using Discount.Application.Commands;
using Discount.Domain.Entities;
using Discount.Grpc.Protos;

namespace Discount.Application.Mappers
{
    public class DiscountMappingProfile : Profile
    {
        public DiscountMappingProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
            CreateMap<CreateDiscountRequest, Coupon>().ReverseMap();
            CreateMap<Coupon, UpdateDiscountCommand>().ReverseMap();
        }
    }
}