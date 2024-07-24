using DemoApp.Api.Common.Mapping;

namespace DemoApp.Api
{
    public static class DependencyInjection
	{
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddMappings();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}

