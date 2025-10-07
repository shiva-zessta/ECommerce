using AutoMapper;
using ECommerce.Dtos;
using ECommerce.Entities;

namespace ECommerce.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(source => source.Category.Name));
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
            CreateMap<Product, CategoryProductDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }

}




