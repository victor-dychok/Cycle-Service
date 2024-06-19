using MediatR;
using ServiceCenter.Application.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Requests.Query.GetCenterById
{
    public class GetCenterByIdQuery : IRequest<ServiceCenterDto>
    {
        public int Id { get; set; }
    }
}
