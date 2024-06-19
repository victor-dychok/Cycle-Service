﻿using MediatR;
using Microsoft.Extensions.Caching.Memory;
using RepairRequest.Application.Abstraction.Percistance;
using RepairRequest.Domain;
using RepairRequest.Service;
using RepairRequest.Service.Exeptions;

namespace RepairRequest.Application.Comands.DeleteRequest
{
    public class DeleteRequestComandHandler : IRequestHandler<DeleteRepairRequestComand, string>
    {
        private readonly IRepository<RepairRequestEntity> _requestRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<RequestStatus> _statuses;
        private readonly MemoryCache _memoryCeche;

        public DeleteRequestComandHandler(
            IRepository<RepairRequestEntity> requests,
            RepairRequestMemoryCache cache,
            IRepository<RequestStatus> statuses,
            ICurrentUserService currentUserService)
        {
            _requestRepository = requests;
            _memoryCeche = cache.Cache;
            _statuses = statuses;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(DeleteRepairRequestComand request, CancellationToken cancellationToken)
        {
            var currentUserId = int.Parse(_currentUserService.CurrentUserId);
            var userRoles = _currentUserService.CurrentUserRoles.ToList();
            var repairRequest = await _requestRepository.SingleOrDefaultAsync(u => u.Id == request.Id);
            var entity = await _requestRepository.SingleOrDefaultAsync(e => e.Id == request.Id);
            if (repairRequest == null)
            {
                throw new NotFoundExeption(new { Id = request.Id });
            }
            if (repairRequest.UserId == currentUserId || repairRequest.MasterId == currentUserId)
            {
                _memoryCeche.Clear();
                var status = new RequestStatus();
                if(userRoles.Contains("Master"))
                {
                    status = await _statuses.SingleOrDefaultAsync(e => e.StatusName == "Сanceled by master");
                }
                else if (userRoles.Contains("Client"))
                {
                    status = await _statuses.SingleOrDefaultAsync(e => e.StatusName == "Сanceled by client");
                }
                if (status is not null)
                {
                    entity.StatusId = status.Id;
                    entity.RequestStatus = status;
                }
                return (await _requestRepository.UpdateAsync(entity, cancellationToken)).RequestStatus.StatusName;
            }
            else
            {
                throw new ForbidenExeption("Access denied");
            }
        }

    }
}