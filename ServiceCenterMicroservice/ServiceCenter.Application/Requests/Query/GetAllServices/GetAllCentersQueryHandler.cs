using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ServiceCenter.Application.Abstraction.Percistance;
using ServiceCenter.Application.dto;
using ServiceCenter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Requests.Query.GetAllServices
{
    public class GetAllCentersQueryHandler : IRequestHandler<GetAllServicesQuery, IReadOnlyCollection<ServiceDto>>
    {

        private readonly IRepository<ServiceEntity> _services;
        private readonly MemoryCache _memoryCeche;
        private readonly IMapper _mapper;
        
        public GetAllCentersQueryHandler(
            IRepository<ServiceEntity> services,
            ServiceCenterMemoryCache cache,
             IMapper mapper
            )
        {
            _services = services;
            _mapper = mapper;
            _memoryCeche = cache.Cache;
        }

        public async Task<IReadOnlyCollection<ServiceDto>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = JsonSerializer.Serialize(request, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
            if (_memoryCeche.TryGetValue(cacheKey, out IReadOnlyCollection<ServiceDto>? result))
            {
                return result!;
            }

            result = _mapper.Map<IReadOnlyCollection<ServiceDto>>(await _services.GetListAsync(
            request.Offset,
            request.Limit,
            request.Name == null ? null : u => u.Name.Contains(request.Name),
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
