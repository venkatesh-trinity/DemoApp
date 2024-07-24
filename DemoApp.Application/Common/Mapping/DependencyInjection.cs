using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace DemoApp.Application.Common.Mapping
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Default.PreserveReference(true);

            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            return services;
        }
    }
}

