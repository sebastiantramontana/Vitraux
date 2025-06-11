using Microsoft.JSInterop;

namespace Vitraux.Execution.Building.JsViewUpdatingInvokation;

internal class JsCreateUpdateFunctionInvoker(IJSRuntime jSRuntime) : IJsCreateUpdateFunctionInvoker
{
    private const string CreateUpdateVersionedFunctionName = "globalThis.vitraux.updating.vmFunctions.createVersionedUpdateViewFunction";
    private const string CreateUpdateNoCachedFunctionName = "globalThis.vitraux.updating.vmFunctions.createUpdateViewFunction";
    private readonly IJSInProcessRuntime _jsInProcessRuntime = (IJSInProcessRuntime)jSRuntime;

    public ValueTask InvokeNoCache(string vmKey, string jsCode)
        => _jsInProcessRuntime.InvokeVoidAsync(CreateUpdateNoCachedFunctionName, vmKey, jsCode);

    public ValueTask InvokeVersionCached(string vmKey, string version, string jsCode)
        => _jsInProcessRuntime.InvokeVoidAsync(CreateUpdateVersionedFunctionName, vmKey, version, jsCode);
}
