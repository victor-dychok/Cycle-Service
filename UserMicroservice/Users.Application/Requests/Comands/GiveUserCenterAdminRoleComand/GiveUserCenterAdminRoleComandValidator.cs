using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Comands.GiveUserAdminRole;

namespace Users.Application.Requests.Comands.GiveUserCenterAdminRoleComand
{
    public class GiveUserCenterAdminRoleComandValidator : AbstractValidator<GiveUserCenterAdminRoleComand>
    {
        public GiveUserCenterAdminRoleComandValidator()
        {

        }
    }
}
