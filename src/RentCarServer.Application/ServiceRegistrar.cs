using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TS.MediatR;

namespace RentCarServer.Application;
public static class ServiceRegistrar
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ServiceRegistrar).Assembly);
            cfg.AddOpenBehavior(typeof(Behaviors.ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(Behaviors.PermissionBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(ServiceRegistrar).Assembly);

        return services;
    }
}
