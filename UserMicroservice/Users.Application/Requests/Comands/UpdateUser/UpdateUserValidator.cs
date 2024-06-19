using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Comands.CreateUser;
using Users.Application.Comands.UpdateUser;
using Users.Application.dto;

namespace Users.Application.Requests.Comands.UpdateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserComand>
    {
        public UpdateUserValidator()
        {
            RuleFor(e => e.Login).MinimumLength(2).MaximumLength(20).WithMessage("must have more than 2 and less than 200 symbols");
            RuleFor(e => e.Phone).Length(13).WithMessage("must contain 13 symbols");
        }
    }
}
