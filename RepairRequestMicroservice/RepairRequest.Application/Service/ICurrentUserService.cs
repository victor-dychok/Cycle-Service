﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequest.Service
{
    public interface ICurrentUserService
    {
        public string CurrentUserId { get; }
        public string[] CurrentUserRoles { get; }
    }
}
