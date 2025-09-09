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
        }
    }

}




