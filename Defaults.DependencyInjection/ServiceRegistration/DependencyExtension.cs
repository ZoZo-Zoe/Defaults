using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Defaults.DependencyInjection.ServiceRegistration;
public static class DependencyExtension {
    public static IServiceCollection AddRegisteredServices(this IServiceCollection services) {
        Type[] types = Assembly.GetCallingAssembly().GetTypes();

        foreach (Type type in types) {
            RegisterServiceAttribute? serviceAttribute = type.GetCustomAttribute<RegisterServiceAttribute>();
            if (serviceAttribute is not null) {
                RegisterService(services, type, serviceAttribute.Lifetime);
            }
        }

        return services;
    }

    private static void RegisterService(IServiceCollection services, Type type, ServiceLifetime lifetime) {
        _ = lifetime switch {
            ServiceLifetime.Singleton => services.AddSingleton(type),
            ServiceLifetime.Scoped => services.AddScoped(type),
            ServiceLifetime.Transient => services.AddTransient(type),
            _ => throw new Exception($"{lifetime} is an invalid lifetime")
        };
    }
}
