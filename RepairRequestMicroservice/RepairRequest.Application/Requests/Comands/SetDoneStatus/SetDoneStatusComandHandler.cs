using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using RepairRequest.Application.Abstraction;
using RepairRequest.Application.Abstraction.Percistance;
using RepairRequest.Application.dto;
using RepairRequest.Domain;
using RepairRequest.Service;
using RepairRequest.Service.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RepairRequest.Application.Requests.Comands.SetDoneStatus
{
    public class SetDoneStatusComandHandler : IRequestHandler<SetDoneStatusComand, GetRequestsDTO>
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly IRepository<RequestStatus> _statuses;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<RepairRequestEntity> _requestsRepository;
        private readonly IRequestCache<IReadOnlyCollection<GetRequestsDTO>> _requestCache;
        private readonly IMqService _mqService;
        private readonly IMapper _mapper;

        public SetDoneStatusComandHandler(
            IRepository<AppUser> users,
            ICurrentUserService currentUserService,
            IRepository<RequestStatus> statuses,
            IRepository<RepairRequestEntity> requests,
            IRequestCache<IReadOnlyCollection<GetRequestsDTO>> requestCache,
            IMqService mqService,
            IMapper mapper)
        {
            _userRepository = users;
            _currentUserService = currentUserService;
            _requestCache = requestCache;

            _mqService = mqService;

            _statuses = statuses;
            _requestsRepository = requests;
            _mapper = mapper;
        }
        public async Task<GetRequestsDTO> Handle(SetDoneStatusComand request, CancellationToken cancellationToken)
        {
            var currentUserId = int.Parse(_currentUserService.CurrentUserId);
            var user = await _userRepository.SingleOrDefaultAsync(e => e.Id == currentUserId);
            var userRoles = _currentUserService.CurrentUserRoles.ToList();
            var repairRequest = await _requestsRepository.SingleOrDefaultAsync(r => r.Id == request.Id);

            if (repairRequest == null)
            {
                throw new NotFoundExeption("Request not found");
            }

            var status = await _statuses.SingleOrDefaultAsync(e => e.StatusName == "Done");
            if (status == null)
            {
                status = await _statuses.AddAsync(new RequestStatus { StatusName = "Completed" });
            }
            if (repairRequest.MasterId == currentUserId && userRoles.Contains("Master"))
            {
                repairRequest.StatusId = status.Id;
            }
            else
            {
                throw new ForbidenExeption("No permision");
            }
            await _requestsRepository.UpdateAsync(repairRequest);
            _mqService.SendMessage("request-status", JsonSerializer.Serialize("Your request status has been changed to " + status.StatusName));
            _requestCache.Clear();
            return _mapper.Map<GetRequestsDTO>(repairRequest);
        }
    }
}
