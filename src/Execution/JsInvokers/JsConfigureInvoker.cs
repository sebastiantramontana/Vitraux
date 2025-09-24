using System.Runtime.InteropServices.JavaScript;

namespace Vitraux.Execution.JsInvokers;

internal partial class JsConfigureInvoker : IJsConfigureInvoker
{
    public void Invoke(bool useShadowDom)
        => Configure(useShadowDom);

    [JSImport("globalThis.vitraux.config.configure")]
    private static partial void Configure(bool useShadowDom);
}