using DemoApp.Application.Common.Interfaces.Persistance;
using DemoApp.Infrastructure.Persistance;
using DemoApp.Infrastructure.Persistance.Interceptors;
using DemoApp.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoApp.Infrastructure
{
    public static class DependencyInjection
	{
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddDbContext(configuration);
            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<PublishDomainEventsInterceptor>();

            services.AddDbContextFactory<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("demoapp");
            });

            return services;
        }
    }
}

