using MediatR;
using ServiceCenter.Application.dto;
using ServiceCenter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Requests.Query.GetAllServices
{
    public class GetAllServicesQuery : BaseNameFilter, IRequest<IReadOnlyCollection<ServiceDto>>
    {
        public int? Offset { get; set; }
        public int? Limit { get; set; }
    }
}
