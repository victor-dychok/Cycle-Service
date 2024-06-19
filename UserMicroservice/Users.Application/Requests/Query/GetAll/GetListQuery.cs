using MediatR;
using Users.Application.dto;

namespace Users.Application.Query.GetAll
{
    public class GetListQuery : BaseUsersFilter, IRequest<IReadOnlyCollection<GetUserDto>>
    {
        public int? Offset { get; set; }
        public int? Limit { get; set; }
    }
}
