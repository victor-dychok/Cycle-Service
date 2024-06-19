using MediatR;
using ServiceCenter.Application.dto;

namespace ServiceCenter.Application.Requests.Comands.DeleteService;

public class DeleteServiceCommand : IRequest<bool>
{
    public int Id { get; set; }
}