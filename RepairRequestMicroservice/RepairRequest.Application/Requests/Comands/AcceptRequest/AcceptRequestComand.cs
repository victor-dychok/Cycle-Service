using MediatR;
using RepairRequest.Application.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequest.Application.Requests.Comands.AcceptRequest
{
    public class AcceptRequestComand : IRequest<GetRequestsDTO>
    {
        public int Id { get; init; }
        public int MasterId { get; init; }

    }
}
