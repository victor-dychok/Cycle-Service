using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ServiceCenter.Application.Abstraction.Percistance;
using ServiceCenter.Application.dto;
using ServiceCenter.Application.Requests.Comands.CreateCenter;
using ServiceCenter.Domain;
using ServiceCenter.Service;
using ServiceCenter.Service.Exeptions;

namespace ServiceCenter.Application.Requests.Comands.CreateService;

public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, ServiceDto>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRepository<ServiceEntity> _services;
    private readonly MemoryCache _memoryCache;
    private readonly IMapper _mapper;
    public CreateServiceCommandHandler(
        ICurrentUserService currentUserService,
        IRepository<ServiceEntity> services,
        ServiceCenterMemoryCache memoryCache,
        IMapper mapper)
    {
        _currentUserService = currentUserService;
        _memoryCache = memoryCache.Cache;
        _mapper = mapper;
        _services = services;
    }

    public async Task<ServiceDto> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var userRoles = _currentUserService.CurrentUserRoles.ToList();

        if (userRoles.Contains("Center Admin"))
        {
            var item = await _services.AddAsync(_mapper.Map<CreateServiceCommand, ServiceEntity>(request));
            return _mapper.Map<ServiceEntity, ServiceDto>(item);
        }
        else
        {
            throw new ForbidenExeption("No permission");
        }
    }
}