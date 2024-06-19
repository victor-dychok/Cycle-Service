using MediatR;
using RepairRequest.Application.dto;
using RepairRequest.Domain;

namespace RepairRequest.Application.Query.GetAll
{
    public class GetListQuery :  IRequest<IReadOnlyCollection<GetRequestsDTO>>
    {
        public int? Offset { get; set; }
        public int? Limit { get; set; }
    }
}
