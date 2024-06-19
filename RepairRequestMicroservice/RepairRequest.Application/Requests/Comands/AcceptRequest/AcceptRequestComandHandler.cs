using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using RepairRequest.Application.Abstraction;
using RepairRequest.Application.Abstraction.Percistance;
using RepairRequest.Application.Comands.CreateRequest;
using RepairRequest.Application.dto;
using RepairRequest.Application.Requests.Comands.UpdateRequest;
using RepairRequest.Domain;
using RepairRequest.Service;
using RepairRequest.Service.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RepairRequest.Application.Requests.Comands.AcceptRequest
{
    public class AcceptRequestComandHandler : IRequestHandler<AcceptRequestComand, GetRequestsDTO>
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly IRepository<AppUserRole> _userRoles;
        private readonly IRepository<AppUserAppRole> _appUR;
        private readonly IRepository<RequestStatus> _statuses;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<RepairRequestEntity> _requestsRepository;
        private readonly MemoryCache _memoryCache;
        private readonly IMqService _mqService;
        private readonly IMapper _mapper;
        public AcceptRequestComandHandler(
            IRepository<AppUser> users,
            IRepository<AppUserRole> userRoles,
            IRepository<AppUserAppRole> appUR,
            ICurrentUserService currentUserService,
            IRepository<RequestStatus> statuses,
            IRepository<RepairRequestEntity> requests,
            RepairRequestMemoryCache memoryCache,
            IMqService mqService,
            IMapper mapper)
        {
            _userRepository = users;
            _userRoles = userRoles;
            _appUR = appUR;
            
            _currentUserService = currentUserService;
            _memoryCache = memoryCache.Cache;
            _mqService = mqService;

            _statuses = statuses;
            _requestsRepository = requests;
            _mapper = mapper;
        }

        public async Task<GetRequestsDTO> Handle(AcceptRequestComand request, CancellationToken cancellationToken)
        {
            var master = await _userRepository.SingleOrDefaultAsync(e => e.Id == request.MasterId);
            if (master is null)
            {
                throw new NotFoundExeption("Master not found");
            }
            var masterRole = await _userRoles.SingleOrDefaultAsync(r => r.Name == "Master");
            var ur = await _appUR.SingleOrDefaultAsync(e => e.UserId == master.Id && e.RoleId == masterRole.Id);
            var currentUserId = int.Parse(_currentUserService.CurrentUserId);
            var userRoles = _currentUserService.CurrentUserRoles.ToList();



            var repairRequest = await _requestsRepository.SingleOrDefaultAsync(u => u.Id == request.Id);
            if (repairRequest is null)
            {
                throw new NotFoundExeption("Request not found");
            }
            repairRequest.MasterId = request.MasterId;
            var status = await _statuses.SingleOrDefaultAsync(e => e.StatusName == "Accepted");
            repairRequest.StatusId = status.Id;
            var item = await _requestsRepository.UpdateAsync(repairRequest, cancellationToken);
            if (item is null)
            {
                throw new BadRequestExeption("Can not update request");
            }
            var customer = await _userRepository.SingleOrDefaultAsync(u => u.Id == repairRequest.UserId);
            if (customer is null)
            {
                throw new NotFoundExeption("Customer not found");
            }
            var emailMessage = new EmailMessageMq
            {
                Email = customer.Email,
                Header = $"Hello, {customer.Login}",
                Message = $"Your request #{repairRequest.Id} status has been changed to {status.StatusName}",
            };
            _mqService.SendMessage("request-status", JsonSerializer.Serialize(emailMessage));
            _memoryCache.Clear();
            return _mapper.Map<GetRequestsDTO>(item);
        }
    }
}
