using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using yuapi.Application.Common.Behaviors;
using yuapi.Application.MappingProfiles;
using yuapi.Application.Services.InterfaceInfos;
using yuapi.Application.Services.Users;
using yuapi.Application.Users.Commands.Register;
using yuapi.Domain.Common;

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
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            // Register FluentValidation validators
            //services.AddValidatorsFromAssemblyContaining<UserRegisterCommandValidator>();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Register pipeline behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
