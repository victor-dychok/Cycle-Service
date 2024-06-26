﻿using Authorization.Application.Abstraction.Percistance;
using Authorization.Domain;
using Authorization.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Percistance
{
    public static class DbContextDi
    {
        public static IServiceCollection AddTodoDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContext, DockerComposeDemoDbContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                }
            );

            services.AddTransient<IContextTransactionCreater, ContextTransactionCreater>();

            services.AddTransient<IRepository<AppUserRole>, SqlServerBaseReository<AppUserRole>>();
            services.AddTransient<IRepository<RefreshToken>, SqlServerBaseReository<RefreshToken>>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IRepository<AppUserAppRole>, SqlServerBaseReository<AppUserAppRole>>();
            services.AddTransient<IRepository<AppUser>, SqlServerBaseReository<AppUser>>();
            return services;
        }
    }
}
