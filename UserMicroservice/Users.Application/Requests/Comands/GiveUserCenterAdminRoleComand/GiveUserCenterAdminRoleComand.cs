using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.dto;

namespace Users.Application.Requests.Comands.GiveUserCenterAdminRoleComand
{
    public class GiveUserCenterAdminRoleComand : IRequest<GetUserDto>
    {
        public int Id { get; set; }
        public bool RemoveRole { get; set; }
    }
}
