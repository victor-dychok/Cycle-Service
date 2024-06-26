using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using User.Application;
using Users.Application.Behaviors;
using Users.Application.dto;
using Users.Application.Mapping;

namespace Users.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
           
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddTransient<IUserCache<GetUserDto>, UserCache<GetUserDto>>();
            services.AddTransient<IUserCache<IReadOnlyCollection<GetUserDto>>, UserCache<IReadOnlyCollection<GetUserDto>>>();

            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() }, includeInternalTypes: true);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ContextTransactionBehavior<,>));
            services.AddTransient<RedisService>();


            return services;
        }
    }
}
