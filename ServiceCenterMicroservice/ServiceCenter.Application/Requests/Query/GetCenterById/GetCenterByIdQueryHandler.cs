using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ServiceCenter.Application.Abstraction.Percistance;
using ServiceCenter.Application.dto;
using ServiceCenter.Domain;
using ServiceCenter.Service.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Requests.Query.GetCenterById
{
    public class GetCenterByIdQueryHandler : IRequestHandler<GetCenterByIdQuery, ServiceCenterDto>
    {

        private readonly IRepository<ServiceCenterEntity> _centers;
        private readonly MemoryCache _memoryCeche;
        private readonly IMapper _mapper;
        public GetCenterByIdQueryHandler(
            IRepository<ServiceCenterEntity> centers,
            ServiceCenterMemoryCache cache,
             IMapper mapper
            )
        {
            _centers = centers;
            _mapper = mapper;
            _memoryCeche = cache.Cache;
        }
        public async Task<ServiceCenterDto> Handle(GetCenterByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = JsonSerializer.Serialize(request, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
            if (_memoryCeche.TryGetValue(cacheKey, out ServiceCenterDto? result))
            {
                return result!;
            }

            var item = await _centers.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item is null)
            {
                throw new NotFoundExeption(new { Id = request.Id });
            }
            result = _mapper.Map<ServiceCenterDto>(item);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                .SetSize(5);
            _memoryCeche.Set(cacheKey, result, cacheEntryOptions);

            return result;
        }
    }
}
