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
        private readonly IRequestCache<GetRequestsDTO> _requestCache;
        private readonly IMapper _mapper;

        public GetByIdOrDefaultQueryHandler(
            IRepository<RepairRequestEntity> repository,
            IRequestCache<GetRequestsDTO> requestCache,
            IMapper mapper)
        {
            _repository = repository;
            _requestCache = requestCache;
            _mapper = mapper;
        }

        public async Task<GetRequestsDTO> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = JsonSerializer.Serialize(request, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
            if (_requestCache.TryGetValue(cacheKey, out GetRequestsDTO? result))
            {
                return result!;
            }

            var item = await _repository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item is null)
            {
                throw new NotFoundExeption(new { Id = request.Id });
            }
            result = _mapper.Map<GetRequestsDTO>(item);

            _requestCache.Set(cacheKey, result);

            return result;
        }
    }
}
