using MediatR;

namespace Users.Application.Comands.DeleteUser
{
    public class DeleteUserComand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
