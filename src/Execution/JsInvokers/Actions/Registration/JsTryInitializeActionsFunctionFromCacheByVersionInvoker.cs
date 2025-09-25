using System.Runtime.InteropServices.JavaScript;
using Vitraux.Execution.JsInvokers.Actions.Registration;

namespace Vitraux.Execution.JsInvokers.Actions;
internal partial class JsTryInitializeActionsFunctionFromCacheByVersionInvoker : IJsTryInitializeActionsFunctionFromCacheByVersionInvoker
{
    public bool Invoke(string vmKey, string version)
       => TryInitializeActionsFunctionFromCacheByVersion(vmKey, version);

    [JSImport("globalThis.vitraux.actions.registration.tryInitializeActionsFunctionFromCacheByVersion")]
    private static partial bool TryInitializeActionsFunctionFromCacheByVersion(string vmKey, string version);
}
