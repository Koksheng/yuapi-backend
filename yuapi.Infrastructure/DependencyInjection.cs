using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Infrastructure.Persistence;

namespace yuapi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register repositories
            services.AddScoped<IInterfaceInfoRepository, InterfaceInfoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddDbContext<DataContext>(opt =>
            {
                //opt.UseSqlServer("Server=.;Database=usercenter;Trusted_Connection=True;TrustServerCertificate=True;");
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}
