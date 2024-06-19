using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Requests.Comands.CreateCenter
{
    public class CreateCenterComandValidator : AbstractValidator<CreateCenterComand>
    {
        public CreateCenterComandValidator() { }
    }
}
