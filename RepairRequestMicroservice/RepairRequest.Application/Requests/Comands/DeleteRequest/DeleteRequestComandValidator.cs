using FluentValidation;
using RepairRequest.Application.Comands.CreateRequest;
using RepairRequest.Application.Comands.DeleteRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequest.Application.Requests.Comands.DeleteRequest
{
    public class DeleteRequestComandValidator : AbstractValidator<DeleteRepairRequestComand>
    {
        public DeleteRequestComandValidator() { }
    }
}
