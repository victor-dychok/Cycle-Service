using AutoMapper;
using MediatR;
using RepairRequest.Application.Abstraction.Percistance;
using RepairRequest.Application.dto;
using RepairRequest.Domain;
using RepairRequest.Service;
using RepairRequest.Service.Exeptions;

namespace RepairRequest.Application.Comands.CreateRequest
{
    public class CreateRequestComandHendler : IRequestHandler<CreateRequestComand, GetRequestsDTO>
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly IRepository<RequestStatus> _statuses;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<RepairRequestEntity> _requestsRepository;
        private readonly IRepository<RepairRequestService> _requestServices;
        private readonly IRepository<RepairRequestComponent> _requestComponents;
        private readonly IRepository<ServiceEntity> _services;
        private readonly IRequestCache<IReadOnlyCollection<GetRequestsDTO>> _requestCache;
        private readonly IMapper _mapper;
        public CreateRequestComandHendler(
            IRepository<AppUser> users,
            ICurrentUserService currentUserService,
            IRepository<RequestStatus> statuses,
            IRepository<RepairRequestService> requestServices,
            IRepository<RepairRequestComponent> requestComponents,
            IRepository<ServiceEntity> services,
            IRepository<RepairRequestEntity> requests,
            IRequestCache<IReadOnlyCollection<GetRequestsDTO>> requestCache,
            IMapper mapper) 
        {
            _userRepository = users;
            _currentUserService = currentUserService;
            _requestCache = requestCache;

            _requestServices = requestServices;
            _requestComponents = requestComponents;
            _services = services;

            _statuses = statuses;
            _requestsRepository = requests;
            _mapper = mapper;
        }

        public async Task<GetRequestsDTO> Handle(CreateRequestComand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.SingleOrDefaultAsync(e => e.Id == request.UserId);
            var currentUserId = int.Parse(_currentUserService.CurrentUserId);

            var userRoles = _currentUserService.CurrentUserRoles.ToList();


            if (user is null || !userRoles.Contains("Client"))
            {
                throw new NotFoundExeption("User not found");
            }

            var entity = new RepairRequestEntity();
            entity.UserId = request.UserId;
            entity.ServiceCenterId = request.ServiceCenterId;

            var status = await _statuses.SingleOrDefaultAsync(e => e.StatusName == "Issued");
            if(status is not null)
            {
                entity.StatusId = status.Id;
                entity.RequestStatus = status;
            }

            var addedItem = await _requestsRepository.AddAsync(entity, cancellationToken);
            if (addedItem is null)
            {
                throw new BadRequestExeption("Can not add request");
            }

            foreach (var item in request.Services)
            {
                await _requestServices.AddAsync(new RepairRequestService
                {
                    RepairRequestId = addedItem.Id,
                    ServiceId = item
                });
            }

            foreach (var item in request.Components)
            {
                await _requestComponents.AddAsync(new RepairRequestComponent
                {
                    RepairRequestId = addedItem.Id,
                    ComponentId = item
                });
            }


            _requestCache.Clear();
            return _mapper.Map<GetRequestsDTO>(addedItem);
        }
    }
}
