using MediatR;

namespace RepairRequest.Application.Comands.DeleteRequest
{
    public class DeleteRepairRequestComand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
