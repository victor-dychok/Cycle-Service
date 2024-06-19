using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Users.Application.Abstraction.Percistance;
using Users.Application.dto;
using Users.Application.Utils;
using Users.Domain;
using Users.Service;
using Users.Service.Exeptions;

namespace Users.Application.Comands.UpdateUserPassword
{
    public class ResetUserPasswordComandHandler : IRequestHandler<ResetPasswordComand, GetUserDto>
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly MemoryCache _memoryCache;
        private readonly ICurrentUserService _currentUserService;

        private readonly IMapper _mapper;
        public ResetUserPasswordComandHandler(
            IRepository<AppUser> users,
            ICurrentUserService currentUserService,
            UserMemoryCache memoryCache,
            IMapper mapper)
        {
            _userRepository = users;
            _currentUserService = currentUserService;
            _memoryCache = memoryCache.Cache;
            _mapper = mapper;
        }

        public async Task<GetUserDto> Handle(ResetPasswordComand request, CancellationToken cancellationToken)
        {
            var currentUserId = int.Parse(_currentUserService.CurrentUserId);
            var userRoles = _currentUserService.CurrentUserRoles.ToList();

            var user = await _userRepository.SingleOrDefaultAsync(u => u.Id == request.Id);
            user.UpdatePassword(PasswordHashUtil.Hash(request.Password));
            if (user.Id == currentUserId || userRoles.Contains("Admin"))
            {
                var item = await _userRepository.UpdateAsync(user, cancellationToken);
                if (item is null)
                {
                    throw new BadRequestExeption("Can not update user");
                }
                _memoryCache.Clear();
                return _mapper.Map<GetUserDto>(item);
            }
            else
            {
                throw new ForbidenExeption("Access denied");
            }
        }
    }
}
