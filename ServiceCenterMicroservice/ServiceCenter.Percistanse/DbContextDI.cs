﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceCenter.Application.Abstraction.Percistance;
using ServiceCenter.Domain;
using ServiceCenter.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Percistance
{
    public static class DbContextDi
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContext, AppDBContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                }
            );

            services.AddTransient<IContextTransactionCreater, ContextTransactionCreater>();

            services.AddTransient<IRepository<AppUserRole>, SqlServerBaseReository<AppUserRole>>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IRepository<AppUserAppRole>, SqlServerBaseReository<AppUserAppRole>>();
            services.AddTransient<IRepository<AppUser>, SqlServerBaseReository<AppUser>>();
            services.AddTransient<IRepository<RepairRequestEntity>, SqlServerBaseReository<RepairRequestEntity>>();
            services.AddTransient<IRepository<Component>, SqlServerBaseReository<Component>>();
            services.AddTransient<IRepository<ServiceEntity>, SqlServerBaseReository<ServiceEntity>>();
            services.AddTransient<IRepository<RepairRequestComponent>, SqlServerBaseReository<RepairRequestComponent>>();
            services.AddTransient<IRepository<RepairRequestService>, SqlServerBaseReository<RepairRequestService>>();
            services.AddTransient<IRepository<RequestStatus>, SqlServerBaseReository<RequestStatus>>();
            services.AddTransient<IRepository<ServiceCenterEntity>, SqlServerBaseReository<ServiceCenterEntity>>();
            services.AddTransient<IRepository<ServiceCenterUser>, SqlServerBaseReository<ServiceCenterUser>>();
            return services;
        }
    }
}
