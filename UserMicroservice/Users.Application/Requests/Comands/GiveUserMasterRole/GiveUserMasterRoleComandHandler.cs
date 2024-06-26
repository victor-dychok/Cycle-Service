using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Abstraction.Percistance;
using Users.Application.Comands.UpdateUser;
using Users.Application.dto;
using Users.Domain;
using Users.Service;
using Users.Service.Exeptions;

namespace Users.Application.Comands.GiveUserMasterRole
{
    public class GiveUserMasterRoleComandHandler : IRequestHandler<GiveUserMasterRoleComand, GetUserDto>
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly IRepository<AppUserAppRole> _appURRepository;
        private readonly IRepository<AppUserRole> _roleRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserCache<IReadOnlyCollection<GetUserDto>> _userCache;
        private readonly IMapper _mapper;

        public GiveUserMasterRoleComandHandler(
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

        public async Task<GetUserDto> Handle(GiveUserMasterRoleComand request, CancellationToken cancellationToken)
        {
            var currentUserId = int.Parse(_currentUserService.CurrentUserId);
            var currentUserRoles = _currentUserService.CurrentUserRoles.ToList();

            var user = await _userRepository.SingleOrDefaultAsync(u => u.Id == request.Id);
            if(user is null)
            {
                throw new NotFoundExeption("User not found");
            }
            if (currentUserRoles.Contains("Admin"))
            {
                var masterRole = await _roleRepository.SingleOrDefaultAsync(e => e.Name == "Master");
                if(masterRole is null)
                {
                    throw new NotFoundExeption("Master role not found");
                }

                if (request.RemoveRole)
                {
                    var role = await _appURRepository.SingleOrDefaultAsync(e => e.UserId == user.Id && e.RoleId == masterRole.Id);
                    if (role is not null)
                    {
                        await _appURRepository.DeleteAsync(role, cancellationToken);
                    }
                    return _mapper.Map<GetUserDto>(user);
                }
                if (await _appURRepository.SingleOrDefaultAsync(e => e.UserId == user.Id && e.RoleId == masterRole.Id) is null)
                {
                    var item = await _appURRepository.AddAsync(new AppUserAppRole
                    {
                        Role = masterRole,
                        RoleId = masterRole.Id,
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
                    throw new BadRequestExeption("Allready master");
                }
                
            }
            else
            {
                throw new ForbidenExeption("Access denied");
            }
        }
    }
}
