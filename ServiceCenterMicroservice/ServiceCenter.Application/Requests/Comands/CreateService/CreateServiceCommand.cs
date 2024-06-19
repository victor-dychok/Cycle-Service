using MediatR;
using ServiceCenter.Application.dto;

namespace ServiceCenter.Application.Requests.Comands.CreateService;

public class CreateServiceCommand : IRequest<ServiceDto>
{
    public string Name { get; init; }
    public double Price { get; init; }
}