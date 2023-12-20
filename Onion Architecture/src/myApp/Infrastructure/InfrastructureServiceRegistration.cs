using Application.Services.ImageService;
using Infrastructure.Adapters.ImageService;
using Infrastructure.LocationOptimizationService;
using Infrastructure.Utilities.HttpClientGenericRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ImageServiceBase, CloudinaryImageServiceAdapter>();
        services.AddScoped<ILocationOptimizationService, LocationOptimizationManager>();
        services.AddScoped<IHttpClientRepository, HttpClientRepository>();
        services.AddHttpClient();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        return services;
    }
}
