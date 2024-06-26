using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Comands.DeleteUser;

namespace Users.Application.Requests.Comands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserComand>
    {
        public DeleteUserCommandValidator() 
        {
            RuleFor(e => e.Id).GreaterThan(0).LessThan(int.MaxValue);
        }
    }
}
