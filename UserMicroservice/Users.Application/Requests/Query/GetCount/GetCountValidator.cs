﻿using Application.User.Application.Query.GetCount;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Query.GetCount
{
    public class GetCountValidator : AbstractValidator<GetCountQuery>
    {
        public GetCountValidator() { }
    }
}