using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Users.Application.Abstraction.Percistance;
using Users.Domain;
using Users.Service;
using Users.Service.Exeptions;

namespace Users.Application.Comands.DeleteUser
{
    public class DeleteUserComandHandler : IRequestHandler<DeleteUserComand, bool>
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly MemoryCache _memoryCeche;

        public DeleteUserComandHandler(
            IRepository<AppUser> users,
            UserMemoryCache cache,
            ICurrentUserService currentUserService)
        {
            _userRepository = users;
            _memoryCeche = cache.Cache;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(DeleteUserComand request, CancellationToken cancellationToken)
        {
            var currentUserId = int.Parse(_currentUserService.CurrentUserId);
            var userRoles = _currentUserService.CurrentUserRoles.ToList();
            var user = await _userRepository.SingleOrDefaultAsync(u => u.Id == request.Id);
            if (user == null)
            {
                throw new NotFoundExeption(new { Id = request.Id });
            }
            if (user.Id == currentUserId || userRoles.Contains("Admin"))
            {
                _memoryCeche.Clear();
                return await _userRepository.DeleteAsync(user, cancellationToken);
            }
            else
            {
                throw new ForbidenExeption("Access denied");
            }
        }

    }
}
