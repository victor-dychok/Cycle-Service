using MediatR;
using ServiceCenter.Application.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Requests.Comands.CreateCenter
{
    public class CreateCenterComand : IRequest<ServiceCenterDto>
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int AdminId { get; set; }
    }
}
