using Authorization.dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Application.Requests.Comands.RefreshJwtToken
{
    public class RefreshJwtTokenComand : IRequest<JwtTokenDto>
    {
        public string RefreshToken { get; set; }
    }
}
