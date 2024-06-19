using Authorization.Api.dto;
using Authorization.Domain;
using Authorization.dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AuthDto, AppUser>();
            CreateMap<AppUser, AuthDto>();
            CreateMap<AppUser, GetUserDto>();
        }
    }
}
