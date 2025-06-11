using Microsoft.JSInterop;

namespace Vitraux.Execution;

internal class JsIsVersionedUpdateViewFunctionRebuildNeededInvoker(IJSRuntime jSRuntime) : IJsIsVersionedUpdateViewFunctionRebuildNeededInvoker
{
    private const string IsVersionedUpdateViewFunctionRebuildNeededName = "globalThis.vitraux.updating.vmFunctions.isVersionedUpdateViewFunctionRebuildNeeded";
    private readonly IJSInProcessRuntime _jsInProcessRuntime = (IJSInProcessRuntime)jSRuntime;

    public ValueTask<bool> IsRebuildNeeded(string vmKey, string version)
        => _jsInProcessRuntime.InvokeAsync<bool>(IsVersionedUpdateViewFunctionRebuildNeededName, vmKey, version);
}