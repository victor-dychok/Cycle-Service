using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ServiceCenter.Application.Abstraction.Percistance;
using ServiceCenter.Domain;
using ServiceCenter.Service;
using ServiceCenter.Service.Exeptions;

namespace ServiceCenter.Application.Requests.Comands.DeleteService;

public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, bool>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRepository<ServiceEntity> _services;
    private readonly MemoryCache _memoryCache;
    public DeleteServiceCommandHandler(
        ICurrentUserService currentUserService,
        IRepository<ServiceEntity> services,
        ServiceCenterMemoryCache memoryCache)
    {
        _currentUserService = currentUserService;
        _memoryCache = memoryCache.Cache;
        _services = services;
    }
    public async Task<bool> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
    {
        var userRoles = _currentUserService.CurrentUserRoles.ToList();

        if (userRoles.Contains("Center Admin"))
        {
            var item = await _services.SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            return await _services.DeleteAsync(item);
        }
        else
        {
            throw new ForbidenExeption("No permission");
        }
    }
}