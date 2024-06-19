using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Requests.Query.GetAllCenters
{
    public class GetAllCentersQueryValidator : AbstractValidator<GetAllCentersQuery>
    {
        public GetAllCentersQueryValidator() 
        {
            RuleFor(x => x.Limit).GreaterThan(0).LessThan(int.MaxValue).When(x => x.Limit.HasValue);
            RuleFor(x => x.Offset).GreaterThan(0).LessThan(int.MaxValue).When(x => x.Offset.HasValue);
            RuleFor(x => x.Name).MaximumLength(100);
        }
    }
}
