using MediatR;
using Users.Application;

namespace Application.User.Application.Query.GetCount
{
    public class GetCountQuery : BaseUsersFilter, IRequest<int>
    {
    }
}
