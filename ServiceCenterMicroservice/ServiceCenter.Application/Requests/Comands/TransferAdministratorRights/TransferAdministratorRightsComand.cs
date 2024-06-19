using MediatR;
using ServiceCenter.Application.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ServiceCenter.Application.Requests.Comands.TransferAdministratorRights
{
    public class TransferAdministratorRightsComand : IRequest<ServiceCenterDto>
    {
        public int ServiceId { get; set; }
        public int AdminId { get; set; }
    }
}
