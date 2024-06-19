using MediatR;
using RepairRequest.Application.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequest.Application.Requests.Comands.DeleteMaster
{
    public class DeleteMasterComand : IRequest<GetRequestsDTO>
    {
        public int Id { get; set; }
    }
}
