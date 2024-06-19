using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ServiceCenter.Application.Abstraction.Percistance;
using ServiceCenter.Application.dto;
using ServiceCenter.Domain;
using ServiceCenter.Service;
using ServiceCenter.Service.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Requests.Comands.CreateCenter
{
    public class CreateCenterComandHandler : IRequestHandler<CreateCenterComand, ServiceCenterDto>
    {

        private readonly IRepository<AppUser> _userRepository;
        private readonly IRepository<AppUserAppRole> _appUR;
        private readonly IRepository<AppUserRole> _roles;
        private readonly IRepository<ServiceCenterUser> _serviceCenterMasters;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<ServiceCenterEntity> _serviceCenters;
        private readonly MemoryCache _memoryCache;
        private readonly IMapper _mapper;
        public CreateCenterComandHandler(
            IRepository<AppUser> users,
            ICurrentUserService currentUserService,
            IRepository<AppUserAppRole> userRoles,
            IRepository<ServiceCenterEntity> serviceCenters,
            IRepository<AppUserRole> roles,
            ServiceCenterMemoryCache memoryCache,
            IMapper mapper)
        {
            _userRepository = users;
            _currentUserService = currentUserService;
            _memoryCache = memoryCache.Cache;
            _appUR = userRoles;
            _mapper = mapper;
            _roles = roles;
            _serviceCenters = serviceCenters;
        }

        public async Task<ServiceCenterDto> Handle(CreateCenterComand request, CancellationToken cancellationToken)
        {
            var userRoles = _currentUserService.CurrentUserRoles.ToList();
            var centerAdmin = await _userRepository.SingleOrDefaultAsync(e => e.Id == request.AdminId);
            var CenterAdminRole = await _roles.SingleOrDefaultAsync(e => e.Name == "Center Admin");

            if (!userRoles.Contains("Admin"))
            {
                throw new BadRequestExeption("You do not have a Admin role");
            }
            if(await _appUR.SingleOrDefaultAsync(e => e.UserId == centerAdmin.Id && e.RoleId == CenterAdminRole.Id) == null)
            {
                throw new BadRequestExeption("User dose not have a Center Admin role");
            }
            else
            {
                var serviceCenter = new ServiceCenterEntity();
                serviceCenter = _mapper.Map<CreateCenterComand, ServiceCenterEntity>(request);

                var addedItem = await _serviceCenters.AddAsync(serviceCenter);

                return _mapper.Map<ServiceCenterDto>(addedItem);
            }
        }
    }
}
