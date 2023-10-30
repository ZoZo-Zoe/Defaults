using Microsoft.Extensions.DependencyInjection;

namespace Defaults.DependencyInjection.ServiceRegistration;


/// <summary>
/// Attribute for adding a service with a specific lifetime. Make sure to call AddRegisteredServices on the IServiceCollection.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class RegisterServiceAttribute : Attribute {
    public ServiceLifetime Lifetime { get; init; }

    public RegisterServiceAttribute(ServiceLifetime lifetime) => Lifetime = lifetime;

}
