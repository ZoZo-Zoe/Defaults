namespace Defaults.DependencyInjection.ParameterInjection;

/// <summary>
/// Attribute for injecting dependencies. Make sure to call AddPropertyInjectedServices on the IServiceCollection.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class InjectAttribute : Attribute {

}
