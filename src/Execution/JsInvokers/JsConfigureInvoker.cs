using Microsoft.JSInterop;

namespace Vitraux.Execution.JsInvokers;

internal class JsConfigureInvoker(IJSRuntime jSRuntime) : IJsConfigureInvoker
{
    private const string FunctionName = "globalThis.vitraux.config.configure";
    private readonly IJSInProcessRuntime _jsInProcessRuntime = (IJSInProcessRuntime)jSRuntime;

    public void Invoke(bool useShadowDom)
        => _jsInProcessRuntime.InvokeVoid(FunctionName, useShadowDom);
}
