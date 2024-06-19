using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using ServiceCenter.Application.Abstraction.Percistance;
using ServiceCenter.Domain;
using ServiceCenter.Service;
using ServiceCenter.Service.Exeptions;
using MediatR;
using ServiceCenter.Application.dto;

namespace ServiceCenter.Application.Requests.Comands.UpdateCenter
{
    public class UpdateCenterCommandHandler : IRequestHandler<UpdateCenterCommand, ServiceCenterDto>
    {
        private readonly IRepository<ServiceCenterUser> _serviceCenterMasters;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<ServiceCenterEntity> _serviceCenters;
        private readonly MemoryCache _memoryCache;
        private readonly IMapper _mapper;
        public UpdateCenterCommandHandler(
            IRepository<AppUser> users,
            ICurrentUserService currentUserService,
            IRepository<AppUserAppRole> userRoles,
            IRepository<ServiceCenterEntity> serviceCenters,
            IRepository<ServiceCenterUser> serviceCenterMasters,
            IRepository<AppUserRole> roles,
            ServiceCenterMemoryCache memoryCache,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _memoryCache = memoryCache.Cache;
            _mapper = mapper;
            _serviceCenterMasters = serviceCenterMasters;
            _serviceCenters = serviceCenters;
        }

        public async Task<ServiceCenterDto> Handle(UpdateCenterCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = int.Parse(_currentUserService.CurrentUserId);
            var userRoles = _currentUserService.CurrentUserRoles.ToList();
            var currentCenter = await _serviceCenters.SingleOrDefaultAsync(e => e.Id == request.Id);

            if (userRoles.Contains("Service Center Admin") && currentCenter.AdminId == currentUserId)
            {

                if (currentCenter == null)
                {
                    throw new BadRequestExeption("No service center with current id");
                }

                var serviceCenterMastersList = await _serviceCenterMasters.GetListAsync();
                foreach (var entity in request.MastersIds)
                {
                    if (serviceCenterMastersList.SingleOrDefault(e => e.UserId == entity && e.CenterId == currentCenter.Id) == null)
                    {
                        _serviceCenterMasters.AddAsync(new ServiceCenterUser
                        {
                            UserId = entity,
                            CenterId = currentCenter.Id,
                        });
                    }
                }

                currentCenter = _mapper.Map<UpdateCenterCommand, ServiceCenterEntity>(request);
                return _mapper.Map<ServiceCenterDto>(await _serviceCenters.UpdateAsync(currentCenter));
            }
            else
            {
                throw new ForbidenExeption("Access denied");
            }
        }
    }
}
