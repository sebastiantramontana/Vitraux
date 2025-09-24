using System.Runtime.InteropServices.JavaScript;
using Vitraux.Execution.JsInvokers.ViewFunctions;

namespace Vitraux.Execution.JsInvokers;

internal partial class JsInitializeNonCachedViewFunctionsInvoker : IJsInitializeNonCachedViewFunctionsInvoker
{
    public Task Invoke(string vmKey, string initializationJsCode, string updateViewJsCode)
        => InitializeNonCachedViewFunctions(vmKey, initializationJsCode, updateViewJsCode);

    [JSImport("globalThis.vitraux.updating.vmFunctions.initializeNonCachedViewFunctions")]
    private static partial Task InitializeNonCachedViewFunctions(string vmKey, string initializationJsCode, string updateViewJsCode);
}
