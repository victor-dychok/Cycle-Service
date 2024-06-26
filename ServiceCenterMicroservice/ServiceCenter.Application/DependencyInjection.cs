using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ServiceCenter.Application.Behaviors;
using ServiceCenter.Application.dto;
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
            services.AddTransient<IServiceCache<ServiceCenterDto>, ServiceCache<ServiceCenterDto>>();
            services.AddTransient<IServiceCache<IReadOnlyCollection<ServiceCenterDto>>, ServiceCache<IReadOnlyCollection<ServiceCenterDto>>>();
            services.AddTransient<IServiceCache<ServiceDto>, ServiceCache<ServiceDto>>();
            services.AddTransient<IServiceCache<IReadOnlyCollection<ServiceDto>>, ServiceCache<IReadOnlyCollection<ServiceDto>>>();

            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() }, includeInternalTypes: true);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ContextTransactionBehavior<,>));
            services.AddTransient<RedisService>();


            return services;
        }
    }
}
