using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Requests.Query.GetCenterById
{
    public class GetCenterByIdQueryValidator : AbstractValidator<GetCenterByIdQuery>
    {
        public GetCenterByIdQueryValidator() { }
    }
}
