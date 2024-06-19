using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequest.Application.Requests.Comands.UpdateRequest
{
    public class UpdateUserValidator : AbstractValidator<UpdateRequestComand>
    {
        public UpdateUserValidator()
        {
        }
    }
}
