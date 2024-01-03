using Microsoft.Extensions.DependencyInjection;
using Application.Users.Common;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
    	services.AddScoped<IPasswordHasherService, PasswordHasherService>();
    	services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
            options.Namespace = "Application.Mediator";
        });

    }
}
