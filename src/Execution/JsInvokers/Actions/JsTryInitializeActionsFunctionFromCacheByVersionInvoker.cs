using System.Runtime.InteropServices.JavaScript;

namespace Vitraux.Execution.JsInvokers.Actions;
internal partial class JsTryInitializeActionsFunctionFromCacheByVersionInvoker : IJsTryInitializeActionsFunctionFromCacheByVersionInvoker
{
    public bool Invoke(string vmKey, string version)
       => TryInitializeActionsFunctionFromCacheByVersion(vmKey, version);

    [JSImport("globalThis.vitraux.actions.tryInitializeActionsFunctionFromCacheByVersion")]
    private static partial bool TryInitializeActionsFunctionFromCacheByVersion(string vmKey, string version);
}
