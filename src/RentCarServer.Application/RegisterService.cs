using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TS.MediatR;

namespace RentCarServer.Application;
public static class RegisterService
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(RegisterService).Assembly);
            cfg.AddOpenBehavior(typeof(Behaviors.ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(Behaviors.PermissionBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(RegisterService).Assembly);

        return services;
    }
}
