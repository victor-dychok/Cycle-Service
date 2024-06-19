using Authorization.Application.Abstraction.Percistance;
using Authorization.Domain;
using Authorization.dto;
using AutoMapper;
using Common.Application.Services.Exeptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Application.Requests.Query
{
    public class GetByIdAsyncComandHandler : IRequestHandler<GetByIdQuery, GetUserDto>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly IMapper _mapper;

        public GetByIdAsyncComandHandler(IRepository<AppUser> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetUserDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _repository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item is null)
            {
                throw new NotFoundExeption(new { request.Id });
            }
            return _mapper.Map<GetUserDto>(item);
        }
    }
}
