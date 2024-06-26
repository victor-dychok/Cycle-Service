using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using MediatR;
using Users.Application.Abstraction.Percistance;
using Users.Domain;
using Users.Application;

namespace Application.User.Application.Query.GetCount
{
    public class GetCountQueryHandler : IRequestHandler<GetCountQuery, int>
    {
        private readonly IRepository<AppUser> _repository;

        public GetCountQueryHandler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(GetCountQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = JsonSerializer.Serialize($"Count: {request}", new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
            int? result;
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
               result = await _repository.CountAsync(token: cancellationToken);
            }
            result = await _repository.CountAsync(u => u.Login.Contains(request.Name));

            return result!.Value;
        }

    }
}
