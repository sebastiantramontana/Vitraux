using Microsoft.JSInterop;

namespace Vitraux.Execution.JsInvokers;

internal class JsInitializeNonCachedViewFunctionsInvoker(IJSRuntime jSRuntime) : IJsInitializeNonCachedViewFunctionsInvoker
{
    private const string FunctionName = "globalThis.vitraux.updating.vmFunctions.initializeNonCachedViewFunctions";
    private readonly IJSInProcessRuntime _jsInProcessRuntime = (IJSInProcessRuntime)jSRuntime;

    public ValueTask Invoke(string vmKey, string initializationJsCode, string updateViewJsCode)
        => _jsInProcessRuntime.InvokeVoidAsync(FunctionName, vmKey, initializationJsCode, updateViewJsCode);
}
