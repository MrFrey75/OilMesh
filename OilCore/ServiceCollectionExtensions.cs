using Microsoft.Extensions.DependencyInjection;
using OilCore.Interfaces;
using OilCore.Services;

namespace OilCore;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all common OilCore services to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to extend.</param>
    /// <param name="applicationName">The application name for logging and context.</param>
    /// <returns>The same IServiceCollection for chaining.</returns>
    public static IServiceCollection AddOilCore(this IServiceCollection services, string applicationName)
    {
        // Centralized logger
        services.AddSingleton(typeof(IOilLoggerService<>), typeof(OilLoggerService<>));

        // App configuration service
        services.AddSingleton<IAppConfigService, AppConfigService>();
        

        return services;
    }
}