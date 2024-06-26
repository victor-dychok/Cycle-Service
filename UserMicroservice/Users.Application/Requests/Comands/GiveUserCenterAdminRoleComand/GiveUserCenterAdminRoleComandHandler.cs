using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Abstraction.Percistance;
using Users.Application.dto;
using Users.Domain;
using Users.Service;
using Users.Service.Exeptions;

namespace Users.Application.Requests.Comands.GiveUserCenterAdminRoleComand
{
    public class GiveUserCenterAdminRoleComandHandler : IRequestHandler<GiveUserCenterAdminRoleComand, GetUserDto>
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly IRepository<AppUserAppRole> _appURRepository;
        private readonly IRepository<AppUserRole> _roleRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserCache<IReadOnlyCollection<GetUserDto>> _userCache;
        private readonly IMapper _mapper;

        public GiveUserCenterAdminRoleComandHandler(
            IRepository<AppUser> userRepository,
            IRepository<AppUserAppRole> appURRepository,
            IRepository<AppUserRole> roleRepository,
            IUserCache<IReadOnlyCollection<GetUserDto>> userCache,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _userCache = userCache;
            _appURRepository = appURRepository;
            _roleRepository = roleRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<GetUserDto> Handle(GiveUserCenterAdminRoleComand request, CancellationToken cancellationToken)
        {
            var currentUserId = int.Parse(_currentUserService.CurrentUserId);
            var currentUserRoles = _currentUserService.CurrentUserRoles.ToList();

            var user = await _userRepository.SingleOrDefaultAsync(u => u.Id == request.Id);
            if (user is null)
            {
                throw new NotFoundExeption("User not found");
            }
            if (currentUserRoles.Contains("Admin"))
            {
                var adminRole = await _roleRepository.SingleOrDefaultAsync(e => e.Name == "Center admin");
                if (adminRole is null)
                {
                    throw new NotFoundExeption("Center Admin role not found");
                }
                if (request.RemoveRole)
                {
                    var role = await _appURRepository.SingleOrDefaultAsync(e => e.UserId == user.Id && e.RoleId == adminRole.Id);
                    if (role is not null)
                    {
                        await _appURRepository.DeleteAsync(role, cancellationToken);
                    }
                    return _mapper.Map<GetUserDto>(user);
                }
                if (await _appURRepository.SingleOrDefaultAsync(e => e.UserId == user.Id && e.RoleId == adminRole.Id) is null)
                {
                    var item = await _appURRepository.AddAsync(new AppUserAppRole
                    {
                        Role = adminRole,
                        RoleId = adminRole.Id,
                        User = user,
                        UserId = user.Id,
                    }, cancellationToken);
                    if (item is null)
                    {
                        throw new BadRequestExeption("Can not update user");
                    }
                    _userCache.Clear();
                    return _mapper.Map<GetUserDto>(user);
                }
                else
                {
                    throw new BadRequestExeption("Allready admin");
                }

            }
            else
            {
                throw new ForbidenExeption("Access denied");
            }
        }
    }
}
