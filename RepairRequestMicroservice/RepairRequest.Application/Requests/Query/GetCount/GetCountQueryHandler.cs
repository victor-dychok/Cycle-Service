using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using MediatR;
using RepairRequest.Domain;
using RepairRequest.Application.Abstraction.Percistance;
using RepairRequest.Application.dto;

namespace RepairRequest.Application.Query.GetCount
{
    public class GetCountQueryHandler : IRequestHandler<GetCountQuery, int>
    {
        private readonly IRepository<RepairRequestEntity> _repository;

        public GetCountQueryHandler(IRepository<RepairRequestEntity> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(GetCountQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = JsonSerializer.Serialize($"Count: {request}", new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });

            var result = await _repository.CountAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                .SetSize(1);

            return result;
        }

    }
}
