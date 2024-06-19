using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using RepairRequest.Application.Abstraction.Percistance;
using RepairRequest.Application.dto;
using RepairRequest.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RepairRequest.Application.Query.GetAll
{
    public class GetListQueryHandler : IRequestHandler<GetListQuery, IReadOnlyCollection<GetRequestsDTO>>
    {
        private readonly IRepository<RepairRequestEntity> _requests;
        private readonly MemoryCache _memoryCeche;
        private readonly IMapper _mapper;

        public GetListQueryHandler(IRepository<RepairRequestEntity> requests, RepairRequestMemoryCache memoryCeche, IMapper mapper) 
        {
            _requests = requests;
            _memoryCeche = memoryCeche.Cache;
            _mapper = mapper;
        }

        async Task<IReadOnlyCollection<GetRequestsDTO>> IRequestHandler<GetListQuery, IReadOnlyCollection<GetRequestsDTO>>.Handle(GetListQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = JsonSerializer.Serialize(request, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
            if (_memoryCeche.TryGetValue(cacheKey, out IReadOnlyCollection<GetRequestsDTO>? result))
            {
                return result!;
            }

            result = _mapper.Map<IReadOnlyCollection<GetRequestsDTO>>(await _requests.GetListAsync(
            request.Offset,
            request.Limit,
            null,
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
