using System.Runtime.InteropServices.JavaScript;
using Vitraux.Execution.JsInvokers.ViewFunctions;

namespace Vitraux.Execution.JsInvokers;

internal partial class JsInitializeNewViewFunctionsToCacheByVersionInvoker : IJsInitializeNewViewFunctionsToCacheByVersionInvoker
{
    public Task Invoke(string vmKey, string version, string initializationJsCode, string updateViewJsCode)
        => InitializeNewViewFunctionsToCacheByVersion(vmKey, version, initializationJsCode, updateViewJsCode);

    [JSImport("globalThis.vitraux.updating.vmFunctions.initializeNewViewFunctionsToCacheByVersion")]
    private static partial Task InitializeNewViewFunctionsToCacheByVersion(string vmKey, string version, string initializationJsCode, string updateViewJsCode);
}