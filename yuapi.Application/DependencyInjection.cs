using Microsoft.Extensions.DependencyInjection;
using yuapi.Application.MappingProfiles;
using yuapi.Application.Services.InterfaceInfos;
using yuapi.Application.Services.Users;

namespace yuapi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Add AutoMapper and register profiles from the Application layer
            services.AddAutoMapper(typeof(MappingProfile));
            // Register application services
            services.AddScoped<IInterfaceInfoService, InterfaceInfoService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
