using MediatR;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using ServiceCenter.Application.Abstraction.Percistance;
using ServiceCenter.Domain;
using ServiceCenter.Service;
using ServiceCenter.Service.Exeptions;
using ServiceCenter.Application.dto;
using ServiceCenter.Application.Requests.Comands.UpdateCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Requests.Comands.DeleteCenter
{
    public class DeleteCenterComandHandler : IRequestHandler<DeleteCenterComand, bool>
    {
        private readonly IRepository<ServiceCenterUser> _serviceCenterMasters;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<ServiceCenterEntity> _serviceCenters;
        private readonly MemoryCache _memoryCache;
        public DeleteCenterComandHandler(
            ICurrentUserService currentUserService,
            IRepository<ServiceCenterEntity> serviceCenters,
            IRepository<ServiceCenterUser> serviceCenterMasters,
            ServiceCenterMemoryCache memoryCache
            ) 
        {
            _currentUserService = currentUserService;
            _memoryCache = memoryCache.Cache;
            _serviceCenterMasters = serviceCenterMasters;
            _serviceCenters = serviceCenters;
        }
        public async Task<bool> Handle(DeleteCenterComand request, CancellationToken cancellationToken)
        {
            var currentUserId = int.Parse(_currentUserService.CurrentUserId);
            var userRoles = _currentUserService.CurrentUserRoles.ToList();
            var currentCenter = await _serviceCenters.SingleOrDefaultAsync(e => e.Id == request.Id);

            if (userRoles.Contains("Service Center Admin") && currentCenter.AdminId == currentUserId)
            {
                var serviceCenterMastersList = await _serviceCenterMasters.GetListAsync();
                foreach(var item in serviceCenterMastersList)
                {
                    if(item.CenterId == currentCenter.Id)
                    {
                        _serviceCenterMasters.DeleteAsync(item);
                    }
                }
                return await _serviceCenters.DeleteAsync(currentCenter);
            }
            else
            {
                throw new ForbidenExeption("Access denied");
            }
        }
    }
}
