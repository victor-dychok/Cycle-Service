using MediatR;
using Users.Application.dto;

namespace Users.Application.Query.GetById
{
    public class GetByIdQuery : IRequest<GetUserDto>
    {
        public int Id { get; set; }
    }
}
