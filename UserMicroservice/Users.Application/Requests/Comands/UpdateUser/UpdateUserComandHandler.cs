using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Users.Application.Abstraction.Percistance;
using Users.Application.dto;
using Users.Domain;
using Users.Service;
using Users.Service.Exeptions;

namespace Users.Application.Comands.UpdateUser
{
    public class UpdateUserComandHandler : IRequestHandler<UpdateUserComand, GetUserDto>
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly IUserCache<IReadOnlyCollection<GetUserDto>> _userCache;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public UpdateUserComandHandler(
            IRepository<AppUser> users,
            ICurrentUserService currentUserService,
            IUserCache<IReadOnlyCollection<GetUserDto>> userCache,
            IMapper mapper)
        {
            _userRepository = users;
            _currentUserService = currentUserService;
            _userCache = userCache;
            _mapper = mapper;
        }

        public async Task<GetUserDto> Handle(UpdateUserComand request, CancellationToken cancellationToken)
        {
            var currentUserId = int.Parse(_currentUserService.CurrentUserId);
            var userRoles = _currentUserService.CurrentUserRoles.ToList();

            var user = await _userRepository.SingleOrDefaultAsync(u => u.Login == request.Login);
            if (user == null)
            {
                throw new NotFoundExeption("User not found");
            }
            user.UpdateLogin(request.Login);
            user.UpdateEmail(request.Email);
            user.UpdatePhone(request.Phone);
            if (user.Id == currentUserId || userRoles.Contains("Admin"))
            {
                _userCache.Clear();
                return _mapper.Map<GetUserDto>(await _userRepository.UpdateAsync(user, cancellationToken));
            }
            else
            {
                throw new ForbidenExeption("Access denied");
            }
        }

    }
}
