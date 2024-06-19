using MediatR;
using RepairRequest.Application.dto;

namespace RepairRequest.Application.Query.GetById
{
    public class GetByIdQuery : IRequest<GetRequestsDTO>
    {
        public int Id { get; set; }
    }
}
