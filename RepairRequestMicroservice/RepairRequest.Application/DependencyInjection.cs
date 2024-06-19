using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RepairRequest.Application.Abstraction;
using RepairRequest.Application.Behaviors;
using RepairRequest.Application.Mapping;
using System.Reflection;

namespace RepairRequest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepairRequestServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
           
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddSingleton<RepairRequestMemoryCache>();

            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() }, includeInternalTypes: true);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ContextTransactionBehavior<,>));
            services.AddTransient<IMqService, MqService>();


            return services;
        }
    }
}
