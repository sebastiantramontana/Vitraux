using Microsoft.JSInterop;

namespace Vitraux.Execution.Building.JsViewUpdatingInvokation;

internal class JsInitializationInvoker(IJSRuntime jSRuntime) : IJsInitializationInvoker
{
    private const string InitializationFunctionName = "globalThis.vitraux.updating.vmFunctions.executeInitializationView";
    private readonly IJSInProcessRuntime _jsInProcessRuntime = (IJSInProcessRuntime)jSRuntime;

    public ValueTask Execute(string jsCode)
        => _jsInProcessRuntime.InvokeVoidAsync(InitializationFunctionName, jsCode);
}
