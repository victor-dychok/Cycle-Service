using AutoMapper;
using ServiceCenter.Application.dto;
using ServiceCenter.Application.Requests.Comands.CreateCenter;
using ServiceCenter.Application.Requests.Comands.CreateService;
using ServiceCenter.Application.Requests.Comands.UpdateCenter;
using ServiceCenter.Application.Requests.Comands.UpdateService;
using ServiceCenter.Domain;

namespace ServiceCenter.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<ServiceCenterEntity, ServiceCenterDto>();
            CreateMap<CreateCenterComand, ServiceCenterEntity>();
            CreateMap<UpdateCenterCommand, ServiceCenterEntity>();
            CreateMap<CreateServiceCommand, ServiceEntity>();
            CreateMap<UpdateServiceCommand, ServiceEntity>();
            CreateMap<ServiceEntity, ServiceDto>();
        }
    }
}
