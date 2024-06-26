using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Comands.GiveUserMasterRole
{
    public class GiveUserMasterRoleComandValidator : AbstractValidator<GiveUserMasterRoleComand>
    {
        public GiveUserMasterRoleComandValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0).LessThan(int.MaxValue);
        }
    }
}
