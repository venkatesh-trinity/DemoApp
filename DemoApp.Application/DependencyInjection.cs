using System.Reflection;
using DemoApp.Application.Common.Behaviours;
using DemoApp.Application.Common.Mapping;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DemoApp.Application
{
    public static class DependencyInjection
	{
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMappings();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}

