using System.Reflection;
using Mapster;
using MapsterMapper;

namespace DemoApp.Api.Common.Mapping
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

