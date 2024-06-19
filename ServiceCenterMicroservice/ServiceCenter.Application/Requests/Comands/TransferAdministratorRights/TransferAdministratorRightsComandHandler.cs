using AutoMapper;
using MediatR;
using ServiceCenter.Application.Abstraction.Percistance;
using ServiceCenter.Application.dto;
using ServiceCenter.Application.Requests.Comands.UpdateCenter;
using ServiceCenter.Domain;
using ServiceCenter.Service;
using ServiceCenter.Service.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Requests.Comands.TransferAdministratorRights
{
    public class TransferAdministratorRightsComandHandler : IRequestHandler<TransferAdministratorRightsComand, ServiceCenterDto>
    {
        private readonly IRepository<ServiceCenterEntity> _serviceCenters;
        private readonly IRepository<AppUser> _userRepository;
        private readonly IRepository<AppUserAppRole> _appUR;
        private readonly IRepository<AppUserRole> _userRoleRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<AppUserRole> _roles;
        private readonly IMapper _mapper;

        public TransferAdministratorRightsComandHandler(
            IRepository<ServiceCenterEntity> serviceCenters,
            IRepository<AppUser> userRepository,
            IRepository<AppUserAppRole> appUR,
            ICurrentUserService currentUserService,
            IRepository<AppUserRole> roles,
            IMapper mapper
            )
        {
            _serviceCenters = serviceCenters;
            _appUR = appUR;
            _roles = roles;
            _userRepository = userRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        public async Task<ServiceCenterDto> Handle(TransferAdministratorRightsComand request, CancellationToken cancellationToken)
        {
            var currentUserId = int.Parse(_currentUserService.CurrentUserId);
            var userRoles = _currentUserService.CurrentUserRoles.ToList();
            var currentCenter = await _serviceCenters.SingleOrDefaultAsync(e => e.Id == request.ServiceId);

            var newAdmin = await _userRepository.SingleOrDefaultAsync(e => e.Id == request.AdminId);
            if (newAdmin == null)
            {
                throw new NotFoundExeption($"User with id {request.AdminId} not found");
            }
            var centerAdminRole = await _roles.SingleOrDefaultAsync(e => e.Name == "Center Admin");
            if (centerAdminRole == null)
            {
                throw new NotFoundExeption("Center admin role not found");
            }
            var newAdminRole = await _appUR.SingleOrDefaultAsync(e => e.RoleId == centerAdminRole.Id && e.UserId == newAdmin.Id);

            if (currentCenter.AdminId == currentUserId && userRoles.Contains("Center Admin") && newAdminRole != null)
            {
                currentCenter.AdminId = request.AdminId;
                return _mapper.Map<ServiceCenterDto>(await _serviceCenters.UpdateAsync(currentCenter));
            }
            else
            {
                throw new ForbidenExeption("Incorrect new admin role or not your cervice senter");
            }
        }
    }
}
