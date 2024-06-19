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
        private readonly MemoryCache _memoryCeche;
        private readonly IMapper _mapper;
         
        public GetByIdOrDefaultQueryHandler(
            IRepository<AppUser> repository,
            UserMemoryCache memoryCeche, 
            IMapper mapper, 
            IRepository<AppUserAppRole> ur)
        {
            _repository = repository;
            _memoryCeche = memoryCeche.Cache;
            _mapper = mapper;
            _ur = ur;
        }

        public async Task<GetUserDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = JsonSerializer.Serialize(request, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
            if (_memoryCeche.TryGetValue(cacheKey, out GetUserDto? result))
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

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                .SetSize(5);
            _memoryCeche.Set(cacheKey, result, cacheEntryOptions);

            return result;
        }
    }
}
