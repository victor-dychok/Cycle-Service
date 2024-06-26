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
        private readonly IRequestCache<IReadOnlyCollection<GetRequestsDTO>> _requestCache;
        private readonly IMapper _mapper;

        public GetListQueryHandler(IRepository<RepairRequestEntity> requests, IRequestCache<IReadOnlyCollection<GetRequestsDTO>> cache, IMapper mapper) 
        {
            _requests = requests;
            _requestCache = cache;
            _mapper = mapper;
        }

        async Task<IReadOnlyCollection<GetRequestsDTO>> IRequestHandler<GetListQuery, IReadOnlyCollection<GetRequestsDTO>>.Handle(GetListQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = JsonSerializer.Serialize(request, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
            if (_requestCache.TryGetValue(cacheKey, out IReadOnlyCollection<GetRequestsDTO>? result))
            {
                return result!;
            }

            result = _mapper.Map<IReadOnlyCollection<GetRequestsDTO>>(await _requests.GetListAsync(
            request.Offset,
            request.Limit,
            null,
            q => q.Id,
            token: cancellationToken));

            _requestCache.Set(cacheKey, result);

            return result;
        }
    }
}
