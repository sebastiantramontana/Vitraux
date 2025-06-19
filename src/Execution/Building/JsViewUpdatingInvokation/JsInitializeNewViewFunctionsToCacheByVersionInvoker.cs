using Microsoft.JSInterop;

namespace Vitraux.Execution.Building.JsViewUpdatingInvokation;

internal class JsInitializeNewViewFunctionsToCacheByVersionInvoker(IJSRuntime jSRuntime) : IJsInitializeNewViewFunctionsToCacheByVersionInvoker
{
    private const string FunctionName = "globalThis.vitraux.updating.vmFunctions.initializeNewViewFunctionsToCacheByVersion";
    private readonly IJSInProcessRuntime _jsInProcessRuntime = (IJSInProcessRuntime)jSRuntime;

    public void Invoke(string vmKey, string version, string initializationJsCode, string updateViewJsCode)
        => _jsInProcessRuntime.InvokeVoid(FunctionName, vmKey, version, initializationJsCode, updateViewJsCode);
}