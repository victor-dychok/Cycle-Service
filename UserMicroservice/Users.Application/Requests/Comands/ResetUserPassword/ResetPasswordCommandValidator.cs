using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Comands.UpdateUserPassword;

namespace Users.Application.Requests.Comands.ResetUserPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordComand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0).LessThan(int.MaxValue);
            RuleFor(e => e.Password).MinimumLength(8).MaximumLength(20).WithMessage("must have more than 8 and less than 200 symbols");
        }
    }
}
