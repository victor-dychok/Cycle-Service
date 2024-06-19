using MediatR;
using RepairRequest.Application.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequest.Application.Requests.Comands.CancelRequest
{
    public class CancelRequestComand : IRequest<GetRequestsDTO>
    {
        public int Id { get; init; }
    }
}
