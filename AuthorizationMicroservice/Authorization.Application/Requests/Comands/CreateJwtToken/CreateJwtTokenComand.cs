using Authorization.dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Application.Requests.Comands.CreateJwtToken
{
    public class CreateJwtTokenComand : IRequest<JwtTokenDto>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
