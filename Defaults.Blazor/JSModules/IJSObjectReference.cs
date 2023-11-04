using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defaults.Blazor;
public class JSObjectReference<T> : IJSObjectReference<T> {
	/// <summary>
	/// Name template for the js module location
	/// </summary>
	private const string _jsPath = "./js/{0}.js";
	/// <summary>
	/// Holds the name of <see cref="T"/>
	/// </summary>
	private readonly string _typeName = typeof(T).FullName ?? throw new Exception("Type name is null");
	/// <summary>
	/// Holds the task of the IJSObjectReference. Always needs to be awaited to ensure the original import InvokeAsync was called correctly.
	/// </summary>
	private readonly Task<IJSObjectReference> _instanceTask;

	public JSObjectReference(IJSRuntime JSRuntime) {
		//wrapped in a Task because the original call is a ValueTask which doesnt support repeated awaiting
		_instanceTask = Task.Run(async () => await JSRuntime.InvokeAsync<IJSObjectReference>("import", string.Format(_jsPath, _typeName)));
	}

	/// <summary>
	/// Get the underlaying <seealso cref="IJSObjectReference"/> thats being used behind the scenes
	/// </summary>
	/// <returns>The underlaying <seealso cref="IJSObjectReference"/></returns>
	public async ValueTask<IJSObjectReference> GetUnderlayingReference() => await _instanceTask;

	public async ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, object?[]? args) => await (await GetUnderlayingReference()).InvokeAsync<TValue>(identifier, args);
	public async ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, CancellationToken cancellationToken, object?[]? args) => await (await GetUnderlayingReference()).InvokeAsync<TValue>(identifier, cancellationToken, args);

	async ValueTask IAsyncDisposable.DisposeAsync() {
		await (await GetUnderlayingReference()).DisposeAsync();
		GC.SuppressFinalize(this);
	}
}

/// <summary>
/// A reference to a javascript module based on the file name. Make sure to call AddJSModules and that the path wwwroot/js exists. The javascript files should have the same name as the class name of <seealso cref="T"/>
/// </summary>
/// <example><![CDATA[
/// public partial class UserPage
/// { 
///		[Inject] public IJSObjectReferene<UserPage> Module { get; set; } = default!
/// }
/// ]]></example>
/// <typeparam name="T"></typeparam>
public interface IJSObjectReference<T> : IJSObjectReference { }