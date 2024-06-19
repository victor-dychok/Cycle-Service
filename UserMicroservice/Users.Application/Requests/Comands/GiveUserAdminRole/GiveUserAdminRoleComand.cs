using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.dto;

namespace Users.Application.Comands.GiveUserAdminRole
{
    public class GiveUserAdminRoleComand : IRequest<GetUserDto>
    {
        public int Id { get; set; }
        public bool RemoveRole { get; set; }
    }
}
