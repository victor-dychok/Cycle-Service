using MediatR;
using ServiceCenter.Application.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Requests.Comands.DeleteCenter
{
    public class DeleteCenterComand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
