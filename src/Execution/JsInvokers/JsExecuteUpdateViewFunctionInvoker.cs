using Microsoft.JSInterop;

namespace Vitraux.Execution.JsInvokers;

internal class JsExecuteUpdateViewFunctionInvoker(IJSRuntime jSRuntime) : IJsExecuteUpdateViewFunctionInvoker
{
    private const string FunctionName = "globalThis.vitraux.updating.vmFunctions.executeUpdateViewFunctionFromJson";
    private readonly IJSInProcessRuntime _jsInProcessRuntime = (IJSInProcessRuntime)jSRuntime;

    public ValueTask Invoke(string vmKey, string json)
        => _jsInProcessRuntime.InvokeVoidAsync(FunctionName, vmKey, json);
}