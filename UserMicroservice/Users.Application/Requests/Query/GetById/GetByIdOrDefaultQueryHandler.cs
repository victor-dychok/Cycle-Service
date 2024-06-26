using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;
using System.Text.Json;
using Users.Application.Query.GetById;
using Users.Application.dto;
using Users.Application.Abstraction.Percistance;
using Users.Domain;
using Users.Application;
using Users.Service.Exeptions;

namespace Application.User.Application.Query.GetById
{
    public class GetByIdOrDefaultQueryHandler : IRequestHandler<GetByIdQuery, GetUserDto>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly IRepository<AppUserAppRole> _ur;
        private readonly IUserCache<GetUserDto> _userCache;
        private readonly IMapper _mapper;
         
        public GetByIdOrDefaultQueryHandler(
            IRepository<AppUser> repository,
            IMapper mapper, 
            IUserCache<GetUserDto> userCache,
            IRepository<AppUserAppRole> ur)
        {
            _repository = repository;
            _userCache = userCache;
            _mapper = mapper;
            _ur = ur;
        }

        public async Task<GetUserDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = JsonSerializer.Serialize(request, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
            if (_userCache.TryGetValue(cacheKey, out GetUserDto? result))
            {
                return result!;
            }

            var item = await _repository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item is null)
            {
                throw new NotFoundExeption(new { Id = request.Id });
            }
            result = _mapper.Map<GetUserDto>(item);
            var roles = await _ur.GetListAsync(null, null, e => e.UserId == item.Id);
            int[] rolesArray = new int[roles.Length];
            for (int i = 0; i < rolesArray.Length; ++i)
            {
                rolesArray[i] = roles[i].RoleId;
            }
            result.Roles = rolesArray;

            _userCache.Set(cacheKey, result);

            return result;
        }
    }
}
