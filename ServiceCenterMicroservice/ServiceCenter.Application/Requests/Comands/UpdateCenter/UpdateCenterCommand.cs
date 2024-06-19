using MediatR;
using ServiceCenter.Application.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Requests.Comands.UpdateCenter
{
    public class UpdateCenterCommand : IRequest<ServiceCenterDto>
    {
        public int Id { get ; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int[] MastersIds { get; set; }
    }
}
