using FluentValidation;
using RepairRequest.Application.Requests.Comands.CancelRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequest.Application.Requests.Comands.SetCompletedStatus
{
    public class SetCompletedStatusComandValidator : AbstractValidator<SetCompletedStatusComand>
    {
        public SetCompletedStatusComandValidator()
        {
        }
    }
}
