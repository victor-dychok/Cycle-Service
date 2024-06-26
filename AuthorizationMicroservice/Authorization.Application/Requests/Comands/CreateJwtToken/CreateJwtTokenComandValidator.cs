using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Application.Requests.Comands.CreateJwtToken
{
    public class CreateJwtTokenComandValidator : AbstractValidator<CreateJwtTokenComand>
    {
        public CreateJwtTokenComandValidator()
        {
            RuleFor(e => e.Login).MinimumLength(2).MaximumLength(20).WithMessage("must have more than 2 and less than 200 symbols");
            RuleFor(e => e.Password).MinimumLength(8).MaximumLength(20).WithMessage("must have more than 8 and less than 200 symbols");
        }
    }
}
