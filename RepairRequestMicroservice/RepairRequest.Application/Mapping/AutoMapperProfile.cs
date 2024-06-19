using AutoMapper;
using RepairRequest.Application.Comands.CreateRequest;
using RepairRequest.Application.dto;
using RepairRequest.Application.Requests.Comands.UpdateRequest;
using RepairRequest.Domain;

namespace RepairRequest.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<RepairRequestEntity, GetRequestsDTO>();
            CreateMap<GetRequestsDTO, RepairRequestEntity>();
            CreateMap<UpdateRequestComand, RepairRequestEntity>();
            CreateMap<CreateRequestComand, RepairRequestEntity>();
        }
    }
}
