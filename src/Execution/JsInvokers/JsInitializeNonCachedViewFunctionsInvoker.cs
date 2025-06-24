using Microsoft.JSInterop;

namespace Vitraux.Execution.JsInvokers;

internal class JsInitializeNonCachedViewFunctionsInvoker(IJSRuntime jSRuntime) : IJsInitializeNonCachedViewFunctionsInvoker
{
    private const string FunctionName = "globalThis.vitraux.updating.vmFunctions.initializeNonCachedViewFunctions";
    private readonly IJSInProcessRuntime _jsInProcessRuntime = (IJSInProcessRuntime)jSRuntime;

    public void Invoke(string vmKey, string initializationJsCode, string updateViewJsCode)
        => _jsInProcessRuntime.InvokeVoid(FunctionName, vmKey, initializationJsCode, updateViewJsCode);
}
