using Microsoft.Extensions.DependencyInjection;

namespace Defaults.Blazor;
public static class JSModulesExtensions {
	/// <summary>
	/// Add the required services to use <seealso cref="IJSObjectReference{T}"/> in blazor pages and classes
	/// </summary>
	/// <param name="services"></param>
	/// <returns><seealso cref="IServiceCollection"/> for chaining</returns>
	public static IServiceCollection AddJSModules(this IServiceCollection services) => services.AddTransient(typeof(IJSObjectReference<>), typeof(JSObjectReference<>));
}