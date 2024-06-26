using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Comands.GiveUserAdminRole
{
    public class GiveUserAdminRoleComandValidator : AbstractValidator<GiveUserAdminRoleComand>
    {
        public GiveUserAdminRoleComandValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0).LessThan(int.MaxValue);
        }
    }
}
