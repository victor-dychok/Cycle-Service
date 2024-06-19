using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using RepairRequest.Application.Abstraction.Percistance;
using RepairRequest.Application.Comands.CreateRequest;
using RepairRequest.Application.dto;
using RepairRequest.Domain;
using RepairRequest.Service;
using RepairRequest.Service.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequest.Application.Requests.Comands.CancelRequest
{
    public class CancelRequestComandHandler : IRequestHandler<CancelRequestComand, GetRequestsDTO>
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly IRepository<RequestStatus> _statuses;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<RepairRequestEntity> _requestsRepository;
        private readonly IRepository<RepairRequestService> _requestServices;
        private readonly IRepository<ServiceEntity> _services;
        private readonly MemoryCache _memoryCache;
        private readonly IMapper _mapper;

        public CancelRequestComandHandler(
            IRepository<AppUser> users,
            ICurrentUserService currentUserService,
            IRepository<RequestStatus> statuses,
            IRepository<RepairRequestEntity> requests,
            RepairRequestMemoryCache memoryCache,
            IMapper mapper)
        {
            _userRepository = users;
            _currentUserService = currentUserService;
            _memoryCache = memoryCache.Cache;

            _statuses = statuses;
            _requestsRepository = requests;
            _mapper = mapper;
        }

        public async Task<GetRequestsDTO> Handle(CancelRequestComand request, CancellationToken cancellationToken)
        {
            var currentUserId = int.Parse(_currentUserService.CurrentUserId);
            var user = await _userRepository.SingleOrDefaultAsync(e => e.Id == currentUserId);
            var userRoles = _currentUserService.CurrentUserRoles.ToList();
            var repairRequest = await _requestsRepository.SingleOrDefaultAsync(r => r.Id == request.Id);

            if (repairRequest == null)
            {
                throw new NotFoundExeption("Request not found");
            }

            if(repairRequest.MasterId == currentUserId && userRoles.Contains("Master"))
            {
                repairRequest.StatusId = (await _statuses.SingleOrDefaultAsync(e => e.StatusName == "Canceled by Master")).Id;
            }
            else if(repairRequest.UserId == currentUserId && userRoles.Contains("Client"))
            {
                repairRequest.StatusId = (await _statuses.SingleOrDefaultAsync(e => e.StatusName == "Canceled by Client")).Id;
            }
            else
            {
                throw new ForbidenExeption("No permision to delete this request");
            }
            await _requestsRepository.UpdateAsync(repairRequest);
            _memoryCache.Clear();
            return _mapper.Map<GetRequestsDTO>(repairRequest);
        }
    }
}
