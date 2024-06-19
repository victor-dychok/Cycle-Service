using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using MediatR;
using RepairRequest.Domain;
using RepairRequest.Application.Abstraction.Percistance;

namespace RepairRequest.Application.Query.GetCount
{
    public class GetCountQueryHandler : IRequestHandler<GetCountQuery, int>
    {
        private readonly IRepository<RepairRequestEntity> _repository;
        private readonly MemoryCache _memoryCeche;

        public GetCountQueryHandler(IRepository<RepairRequestEntity> repository, RepairRequestMemoryCache memoryCeche)
        {
            _repository = repository;
            _memoryCeche = memoryCeche.Cache;
        }

        public async Task<int> Handle(GetCountQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = JsonSerializer.Serialize($"Count: {request}", new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
            if (_memoryCeche.TryGetValue(cacheKey, out int? result))
            {
                return result!.Value;
            }
            result = await _repository.CountAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                .SetSize(1);
            _memoryCeche.Set(cacheKey, result, cacheEntryOptions);

            return result!.Value;
        }

    }
}
