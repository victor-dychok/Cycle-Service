﻿using AutoMapper;
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
        private readonly MemoryCache _memoryCeche;
        private readonly IMapper _mapper;

        public GetListQueryHandler(IRepository<AppUser> users, UserMemoryCache memoryCeche, IMapper mapper) 
        {
            _users = users;
            _memoryCeche = memoryCeche.Cache;
            _mapper = mapper;
        }
        public async Task<IReadOnlyCollection<GetUserDto>> Handle(GetListQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = JsonSerializer.Serialize(request, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
            if (_memoryCeche.TryGetValue(cacheKey, out IReadOnlyCollection<GetUserDto>? result))
            {
                return result!;
            }

            result = _mapper.Map<IReadOnlyCollection<GetUserDto>>(await _users.GetListAsync(
            request.Offset,
            request.Limit,
            request.Name == null ? null : u => u.Login.Contains(request.Name),
            q => q.Id,
            token: cancellationToken));

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                .SetSize(5);
            _memoryCeche.Set(cacheKey, result, cacheEntryOptions);

            return result;
        }
    }
}
