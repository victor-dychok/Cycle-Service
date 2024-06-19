using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ServiceCenter.Application.Abstraction.Percistance;
using ServiceCenter.Application.dto;
using ServiceCenter.Domain;
using ServiceCenter.Service;
using ServiceCenter.Service.Exeptions;

namespace ServiceCenter.Application.Requests.Comands.UpdateService;

public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, ServiceDto>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRepository<ServiceEntity> _services;
    private readonly MemoryCache _memoryCache;
    private readonly IMapper _mapper;
    
    public UpdateServiceCommandHandler(
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
    
    public async Task<ServiceDto> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {var userRoles = _currentUserService.CurrentUserRoles.ToList();

        if (userRoles.Contains("Center Admin"))
        {
            var item = await _services.SingleOrDefaultAsync(e => e.Id == request.Id);
            item = _mapper.Map<UpdateServiceCommand, ServiceEntity>(request);
            return _mapper.Map<ServiceEntity, ServiceDto>(await _services.UpdateAsync(item));
        }
        else
        {
            throw new ForbidenExeption("No permission");
        }
    }
}