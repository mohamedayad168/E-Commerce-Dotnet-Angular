using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace E_Commerce.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(m => m.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(m => m.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(m => m.PictureUrl, o => o.MapFrom<ProductUrlResolver>()).ReverseMap();
        }
    }
}