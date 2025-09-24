using System.Runtime.InteropServices.JavaScript;
using Vitraux.Execution.JsInvokers.ViewFunctions;

namespace Vitraux.Execution.JsInvokers;

internal partial class JsTryInitializeViewFunctionsFromCacheByVersionInvoker : IJsTryInitializeViewFunctionsFromCacheByVersionInvoker
{
    public Task<bool> Invoke(string vmKey, string version)
        => TryInitializeViewFunctionsFromCacheByVersion(vmKey, version);

    [JSImport("globalThis.vitraux.updating.vmFunctions.tryInitializeViewFunctionsFromCacheByVersion")]
    private static partial Task<bool> TryInitializeViewFunctionsFromCacheByVersion(string vmKey, string version);
}
