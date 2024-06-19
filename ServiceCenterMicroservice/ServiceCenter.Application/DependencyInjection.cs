using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ServiceCenter.Application.Behaviors;
using ServiceCenter.Application.Mapping;
using System.Reflection;

namespace ServiceCenter.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceCenterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
           
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddSingleton<ServiceCenterMemoryCache>();

            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() }, includeInternalTypes: true);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ContextTransactionBehavior<,>));


            return services;
        }
    }
}
