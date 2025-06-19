using Microsoft.JSInterop;

namespace Vitraux.Execution.Building.JsViewUpdatingInvokation;

internal class JsTryInitializeViewFunctionsFromCacheByVersionInvoker(IJSRuntime jSRuntime) : IJsTryInitializeViewFunctionsFromCacheByVersionInvoker
{
    private const string FunctionName = "globalThis.vitraux.updating.vmFunctions.tryInitializeViewFunctionsFromCacheByVersion";
    private readonly IJSInProcessRuntime _jsInProcessRuntime = (IJSInProcessRuntime)jSRuntime;

    public ValueTask<bool> Invoke(string vmKey, string version)
        => _jsInProcessRuntime.InvokeAsync<bool>(FunctionName, vmKey, version);
}
