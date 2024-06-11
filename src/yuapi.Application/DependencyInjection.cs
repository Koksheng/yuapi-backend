using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using yuapi.Application.Common.Behaviors;
using yuapi_client_sdkyuapi_client_sdk.Client;

namespace yuapi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Add AutoMapper and register profiles from the Application layer
            //services.AddAutoMapper(typeof(MappingProfile));

            // Register AutoMapper and scan for profiles
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Register application services
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            // Register FluentValidation validators and scan for validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Register pipeline behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Register HttpClient and YuApiClient
            services.AddHttpClient<YuApiClient>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:8123"); // Adjust the base address as needed
            });

            return services;
        }
    }
}
