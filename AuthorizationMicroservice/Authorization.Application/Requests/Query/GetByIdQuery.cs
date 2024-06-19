using Authorization.dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Application.Requests.Query
{
    public class GetByIdQuery : IRequest<GetUserDto>
    {
        public int Id { get; set; }
    }
}
