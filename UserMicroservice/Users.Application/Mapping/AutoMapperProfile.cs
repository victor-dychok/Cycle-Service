using AutoMapper;
using Users.Application.Comands.CreateUser;
using Users.Application.Comands.UpdateUser;
using Users.Application.dto;
using Users.Domain;

namespace Users.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<CreateUserComand, AppUser>();

            CreateMap<GetUserDto, AppUser>();
            CreateMap<AppUser, GetUserDto>();

            CreateMap<UpdateUserComand, AppUser>();
        }
    }
}
