using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using System.Text.Json.Serialization;
using Users.Application.Abstraction.Percistance;
using Users.Application.dto;
using Users.Domain;

namespace Users.Application.Query.GetAll
{
    public class GetListQueryHandler : IRequestHandler<GetListQuery, IReadOnlyCollection<GetUserDto>>
    {
        private readonly IRepository<AppUser> _users;
        private readonly IUserCache<IReadOnlyCollection<GetUserDto>> _userCache;
        private readonly IMapper _mapper;

        public GetListQueryHandler(IRepository<AppUser> users, IUserCache<IReadOnlyCollection<GetUserDto>> cache, IMapper mapper) 
        {
            _users = users;
            _userCache = cache;
            _mapper = mapper;
        }
        public async Task<IReadOnlyCollection<GetUserDto>> Handle(GetListQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = JsonSerializer.Serialize(request, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
            if (_userCache.TryGetValue(cacheKey, out IReadOnlyCollection<GetUserDto>? result))
            {
                return result!;
            }

            result = _mapper.Map<IReadOnlyCollection<GetUserDto>>(await _users.GetListAsync(
            request.Offset,
            request.Limit,
            request.Name == null ? null : u => u.Login.Contains(request.Name),
            q => q.Id,
            token: cancellationToken));


            _userCache.Set(cacheKey, result);

            return result;
        }
    }
}
