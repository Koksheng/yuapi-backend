using Microsoft.Extensions.DependencyInjection;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Infrastructure.Persistence;

namespace yuapi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Register repositories
            services.AddScoped<IInterfaceInfoRepository, InterfaceInfoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
