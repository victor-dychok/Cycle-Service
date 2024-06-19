using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;
using System.Text.Json;
using RepairRequest.Application.dto;
using RepairRequest.Domain;
using RepairRequest.Application.Abstraction.Percistance;
using System.Net.Cache;
using RepairRequest.Service.Exeptions;

namespace RepairRequest.Application.Query.GetById
{
    public class GetByIdOrDefaultQueryHandler : IRequestHandler<GetByIdQuery, GetRequestsDTO>
    {
        private readonly IRepository<RepairRequestEntity> _repository;
        private readonly MemoryCache _memoryCeche;
        private readonly IMapper _mapper;

        public GetByIdOrDefaultQueryHandler(IRepository<RepairRequestEntity> repository, RepairRequestMemoryCache memoryCeche, IMapper mapper)
        {
            _repository = repository;
            _memoryCeche = memoryCeche.Cache;
            _mapper = mapper;
        }

        public async Task<GetRequestsDTO> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = JsonSerializer.Serialize(request, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
            if (_memoryCeche.TryGetValue(cacheKey, out GetRequestsDTO? result))
            {
                return result!;
            }

            var item = await _repository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item is null)
            {
                throw new NotFoundExeption(new { Id = request.Id });
            }
            result = _mapper.Map<GetRequestsDTO>(item);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                .SetSize(5);
            _memoryCeche.Set(cacheKey, result, cacheEntryOptions);

            return result;
        }
    }
}
