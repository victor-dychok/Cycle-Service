using MediatR;
using RepairRequest.Application.dto;

namespace RepairRequest.Application.Requests.Comands.UpdateRequest
{
    public class UpdateRequestComand : IRequest<GetRequestsDTO>
    {
        public int Id { get; set; }
        public int UserId { get; init; }
        public int MasterId { get; init; }
    }
}
