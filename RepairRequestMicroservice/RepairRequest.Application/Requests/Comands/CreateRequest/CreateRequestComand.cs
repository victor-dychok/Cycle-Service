using MediatR;
using RepairRequest.Application.dto;

namespace RepairRequest.Application.Comands.CreateRequest
{
    public class CreateRequestComand : IRequest<GetRequestsDTO>
    {
        public int UserId { get; init; }
        public int[] Services { get; init; }
        public int[] Components { get; init; }
        public int ServiceCenterId { get; set; }
    }
}
