using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Defaults.DependencyInjection.Extensions;
public static class IServiceProviderExtensions {
    /// <summary>
    /// Deserializes requested section to a Class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <param name="section"></param>
    /// <returns><see cref="{T}"/> from config</returns>
    public static T FromConfiguration<T>(this IServiceProvider serviceProvider, string section) => serviceProvider.GetRequiredService<IConfiguration>().GetRequiredSection(section).Get<T>()!;

    /// <summary>
    /// Adds <seealso cref="{TOptions}"/> as a singleton and fills the properties from the config
    /// </summary>
    /// <typeparam name="TOption"></typeparam>
    /// <param name="serviceCollection"></param>
    /// <param name="section"></param>
    /// <returns><see cref="IServiceCollection"/> for chaining</returns>
    public static IServiceCollection AddOption<TOption>(this IServiceCollection serviceCollection, string section) where TOption : class => serviceCollection.AddSingleton(sp => sp.FromConfiguration<TOption>(section));
}