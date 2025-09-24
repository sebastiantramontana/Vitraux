using System.Runtime.InteropServices.JavaScript;
using Vitraux.Execution.JsInvokers.ViewFunctions;

namespace Vitraux.Execution.JsInvokers;

internal partial class JsExecuteUpdateViewFunctionInvoker : IJsExecuteUpdateViewFunctionInvoker
{
    public Task Invoke(string vmKey, string json)
        => ExecuteUpdateViewFunctionFromJson(vmKey, json);

    [JSImport("globalThis.vitraux.updating.vmFunctions.executeUpdateViewFunctionFromJson")]
    private static partial Task ExecuteUpdateViewFunctionFromJson(string vmKey, string json);
}