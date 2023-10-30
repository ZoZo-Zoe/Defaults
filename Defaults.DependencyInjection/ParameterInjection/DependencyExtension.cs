using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Defaults.DependencyInjection.ParameterInjection;
public static class DependencyExtension {
    private readonly static string PROPERTY_NAME_OF_TYPE_IN_TARGET_IN_FACTORY_METHOD = "serviceImplementationType";

    /// <summary>
    /// Add property injectable dependencies for already added services. Call this after registering services.
    /// </summary>
    /// <param name="services">Services</param>
    /// <returns><see cref="IServiceCollection"/> for chaining</returns>
    public static IServiceCollection AddPropertyInjectedServices(this IServiceCollection services) {
        // Get array for independent iteration
        ServiceDescriptor[] servicesList = services.ToArray();

        // Search services with injectable properties to add properties assigning
        foreach (ServiceDescriptor service in servicesList) {
            Func<IServiceProvider, object>? implementationFactory = service?.ImplementationFactory;
            object? implementationInstance = service?.ImplementationInstance;

            // Getting dependency implementation type
            Type? implementationType = service?.ImplementationType ??
                implementationFactory?.GetType()
                    ?.GenericTypeArguments
                    ?.LastOrDefault() ??
                implementationInstance?.GetType();

            // Getting type from factory method if it's not gotten earlier
            if (implementationType == typeof(object)) {
                implementationType = implementationFactory?.Target
                    ?.GetType()
                    ?.GetField(PROPERTY_NAME_OF_TYPE_IN_TARGET_IN_FACTORY_METHOD)
                    ?.GetValue(implementationFactory?.Target) as Type;
            }

            if (implementationType is null) {
                continue;
            }

            if (!TryGetInjectableProperties(implementationType, out var injectableProperties))
                continue;

            ImplementationType implementation = GetImplementationType(implementationFactory, implementationInstance, implementationType);

            // Getting service descriptor parameters
            Type serviceType = service?.ServiceType ?? implementationType;
            ServiceLifetime serviceLifeTime = service!.Lifetime;

            // Replacing dependency in service collection
            services.Replace(new ServiceDescriptor(serviceType, serviceProvider => {

                // Creating instance
                object serviceInstance = implementation switch {
                    ImplementationType.ByFactory => CreateByFactory(implementationFactory!, serviceProvider),
                    ImplementationType.ByImplementation => CreateByImplementation(implementationInstance!),
                    ImplementationType.ByDI => CreateByDI(implementationType, serviceProvider),
                    _ => throw new ArgumentException("Invalid implementation type", nameof(implementation))
                };

                // Assigning dependencies
                foreach (PropertyInfo injectableProperty in injectableProperties) {
                    // Get dependency implementation
                    object? dependencyInstance = serviceProvider.GetService(injectableProperty.PropertyType) ??
                        throw new InvalidOperationException($"Cannot provide value for property: '{injectableProperty.Name}' on type '{serviceInstance?.GetType()?.FullName}'. No service for type '{injectableProperty.PropertyType.FullName}' has been registered.");

                    // Set dependency in property
                    injectableProperty.SetValue(serviceInstance, dependencyInstance);
                }

                return serviceInstance;
            }, serviceLifeTime));
        }

        return services;
    }

    private static object CreateByFactory(Func<IServiceProvider, object> implementationFactory, IServiceProvider services) {
        object serviceInstance = implementationFactory.Invoke(services);
        return serviceInstance;
    }
    private static object CreateByImplementation(object implementationInstance) {
        return implementationInstance;
    }
    private static object CreateByDI(Type implementationType, IServiceProvider services) {
        ConstructorInfo constructor = implementationType.GetConstructors()[0];
        Span<ParameterInfo> parameters = constructor.GetParameters();

        Span<object> span = new object[parameters.Length];
        for (var i = 0; i < parameters.Length; i++) {
            span[i] = services.GetRequiredService(parameters[i].ParameterType);
        }
        object serviceInstance = Activator.CreateInstance(implementationType, span.ToArray())!;
        return serviceInstance!;
    }
    private static bool TryGetInjectableProperties(Type? implementationType, out PropertyInfo[] injectableProperties) {
        Span<PropertyInfo> properties = implementationType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        Span<PropertyInfo> injectablePropertiesSpan = new PropertyInfo[properties.Length];
        int totalCount = 0;
        for (var i = 0; i < properties.Length; i++) {
            if (!properties[i].CanWrite)
                continue;

            if (!properties[i].GetCustomAttributes<InjectAttribute>(false).Any())
                continue;

            injectablePropertiesSpan[totalCount] = properties[i];
            totalCount++;
        }

        injectableProperties = injectablePropertiesSpan[..totalCount].ToArray();
        return totalCount > 0;
    }

    private static ImplementationType GetImplementationType(Func<IServiceProvider, object>? implementationFactory, object? implementationInstance, Type? implementationType) {
        if (implementationFactory is not null) {
            return ImplementationType.ByFactory;
        }
        else if (implementationInstance is not null) {
            return ImplementationType.ByImplementation;
        }
        else {
            return ImplementationType.ByDI;
        }
    }

    private enum ImplementationType {
        ByFactory,
        ByImplementation,
        ByDI
    }
}