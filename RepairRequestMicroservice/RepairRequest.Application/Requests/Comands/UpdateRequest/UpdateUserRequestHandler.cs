using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using RepairRequest.Application;
using RepairRequest.Application.Abstraction.Percistance;
using RepairRequest.Application.dto;
using RepairRequest.Domain;
using RepairRequest.Service;
using RepairRequest.Service.Exeptions;

namespace RepairRequest.Application.Requests.Comands.UpdateRequest
{
    public class UpdateUserRequestHandler : IRequestHandler<UpdateRequestComand, GetRequestsDTO>
    {
        private readonly IRepository<RepairRequestEntity> _requestsRepository;
        private readonly IRequestCache<IReadOnlyCollection<GetRequestsDTO>> _requestCache;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public UpdateUserRequestHandler(
            IRepository<RepairRequestEntity> requestsRepository,
            ICurrentUserService currentUserService,
            IRequestCache<IReadOnlyCollection<GetRequestsDTO>> requestCache,
            IMapper mapper)
        {
            _requestsRepository = requestsRepository;
            _currentUserService = currentUserService;
            _requestCache = requestCache;
            _mapper = mapper;
        }

        public async Task<GetRequestsDTO> Handle(UpdateRequestComand request, CancellationToken cancellationToken)
        {
            var currentUserId = int.Parse(_currentUserService.CurrentUserId);
            var userRoles = _currentUserService.CurrentUserRoles.ToList();

            var repairRequest = await _requestsRepository.SingleOrDefaultAsync(u => u.Id == request.Id);
            repairRequest = _mapper.Map<UpdateRequestComand, RepairRequestEntity>(request);
            if (repairRequest.Id == currentUserId || userRoles.Contains("Admin"))
            {
                var item = await _requestsRepository.UpdateAsync(repairRequest, cancellationToken);
                if (item is null)
                {
                    throw new BadRequestExeption("Can not update user");
                }
                _requestCache.Clear();
                return _mapper.Map<GetRequestsDTO>(item);
            }
            else
            {
                throw new ForbidenExeption("Access denied");
            }
        }

    }
}
