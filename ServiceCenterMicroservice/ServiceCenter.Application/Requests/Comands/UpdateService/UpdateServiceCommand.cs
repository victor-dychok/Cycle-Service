using MediatR;
using ServiceCenter.Application.dto;

namespace ServiceCenter.Application.Requests.Comands.UpdateService;

public class UpdateServiceCommand : IRequest<ServiceDto>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public double Price { get; init; }
}