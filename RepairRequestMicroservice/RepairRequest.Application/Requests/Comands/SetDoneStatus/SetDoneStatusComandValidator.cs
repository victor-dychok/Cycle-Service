using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequest.Application.Requests.Comands.SetDoneStatus
{
    public class SetDoneStatusComandValidator : AbstractValidator<SetDoneStatusComand>
    {
        public SetDoneStatusComandValidator()
        {
        }
    }
}
