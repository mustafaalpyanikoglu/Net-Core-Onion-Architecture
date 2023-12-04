using Application.Services.AuthService;
using Application.Services.CustomerService;
using Application.Services.LocationSolverService;
using Application.Services.UserService;
using Application.Services.WarehouseService;
using Core.Application.Algorithms;
using Core.Application.Pipelines.Logging;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Logger;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<IUserService, UserManager>();
        services.AddScoped<IWarehouseService, WarehouseManager>();
        services.AddScoped<ICustomerService, CustomerManager>();
        services.AddScoped<ICustomerWarehouseCostService, CustomerWarehouseCostManager>();
        services.AddScoped<ILocationSolverService, LocationSolverManager>();

        services.AddScoped<ISimulatedAnnealing, SimulatedAnnealing>();
        services.AddScoped<IQuickSort, QuickSort>();

        services.AddSingleton<LoggerServiceBase, MsSqlLogger>();
        //services.AddSingleton<LoggerServiceBase, FileLogger>();

        services.AddSingleton<IMailService, MailKitMailService>();

        return services;
    }

    public static IServiceCollection AddSubClassesOfType(
    this IServiceCollection services,
    Assembly assembly,
    Type type,
    Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null)
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (var item in types)
        {
            if (addWithLifeCycle == null)
            {
                services.AddScoped(item);
            }
            else
            {
                addWithLifeCycle(services, type);
            }
        }
        return services;
    }
}
