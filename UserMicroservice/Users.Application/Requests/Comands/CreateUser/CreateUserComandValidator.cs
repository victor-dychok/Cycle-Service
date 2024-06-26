using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Comands.CreateUser;

namespace User.Application.Comands.CreateUser
{
    public class CreateUserComandValidator : AbstractValidator<CreateUserComand>
    {
        public CreateUserComandValidator()
        {
            RuleFor(e => e.Login).MinimumLength(2).MaximumLength(20).WithMessage("must have more than 2 and less than 200 symbols");
            RuleFor(e => e.Password).MinimumLength(8).MaximumLength(20).WithMessage("must have more than 8 and less than 200 symbols");
            RuleFor(e => e.Phone).Length(13).WithMessage("must contain 13 symbols");
        }
    }
}
