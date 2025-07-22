using Microsoft.JSInterop;

namespace Vitraux.Execution.JsInvokers;

internal class JsInitializeNewViewFunctionsToCacheByVersionInvoker(IJSRuntime jSRuntime) : IJsInitializeNewViewFunctionsToCacheByVersionInvoker
{
    private const string FunctionName = "globalThis.vitraux.updating.vmFunctions.initializeNewViewFunctionsToCacheByVersion";
    private readonly IJSInProcessRuntime _jsInProcessRuntime = (IJSInProcessRuntime)jSRuntime;

    public ValueTask Invoke(string vmKey, string version, string initializationJsCode, string updateViewJsCode)
        => _jsInProcessRuntime.InvokeVoidAsync(FunctionName, vmKey, version, initializationJsCode, updateViewJsCode);
}