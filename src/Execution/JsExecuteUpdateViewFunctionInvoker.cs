using Microsoft.JSInterop;

namespace Vitraux.Execution;

internal class JsExecuteUpdateViewFunctionInvoker(IJSRuntime jSRuntime) : IJsExecuteUpdateViewFunctionInvoker
{
    private const string FunctionName = "globalThis.vitraux.updating.vmFunctions.executeUpdateViewFunction";
    private readonly IJSInProcessRuntime _jsInProcessRuntime = (IJSInProcessRuntime)jSRuntime;

    public ValueTask Invoke(string vmKey, string json)
        => _jsInProcessRuntime.InvokeVoidAsync(FunctionName, vmKey, json);
}