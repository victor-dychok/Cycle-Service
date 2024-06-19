using MediatR;
using Users.Application.dto;

namespace Users.Application.Comands.UpdateUserPassword
{
    public class ResetPasswordComand : IRequest<GetUserDto>
    {
        public int Id { get; set; }
        public string Password { get; set; }
    }
}
