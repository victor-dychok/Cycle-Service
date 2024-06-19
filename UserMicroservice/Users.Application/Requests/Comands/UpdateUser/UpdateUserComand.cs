using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.dto;

namespace Users.Application.Comands.UpdateUser
{
    public class UpdateUserComand : IRequest<GetUserDto>
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
