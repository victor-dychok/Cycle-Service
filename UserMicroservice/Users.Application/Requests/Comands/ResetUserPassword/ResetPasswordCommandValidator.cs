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
        public ResetPasswordCommandValidator() { }
    }
}
