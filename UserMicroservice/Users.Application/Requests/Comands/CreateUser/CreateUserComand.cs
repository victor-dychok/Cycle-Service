
using MediatR;
using Users.Application.dto;

namespace Users.Application.Comands.CreateUser
{
    public class CreateUserComand : IRequest<GetUserDto>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
