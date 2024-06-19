using FluentValidation;
using ServiceCenter.Application.Requests.Comands.CreateCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Requests.Comands.UpdateCenter
{
    public class UpdateCenterCommandValidator : AbstractValidator<UpdateCenterCommand>
    {
        public UpdateCenterCommandValidator() 
        {
            RuleFor(e => e.Name).MinimumLength(5).MaximumLength(100).WithMessage("must have more than 5 and less than 100 symbols");
            RuleFor(e => e.Location).MinimumLength(20).MaximumLength(200).WithMessage("must have more than 20 and less than 200 symbols");
        }
    }
}
