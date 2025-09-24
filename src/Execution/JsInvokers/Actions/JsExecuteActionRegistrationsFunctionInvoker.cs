using System.Runtime.InteropServices.JavaScript;

namespace Vitraux.Execution.JsInvokers.Actions;

internal partial class JsExecuteActionRegistrationsFunctionInvoker : IJsExecuteActionRegistrationsFunctionInvoker
{
    public void Invoke(string vmKey)
       => ExecuteActionRegistrationsFunction(vmKey);

    [JSImport("globalThis.vitraux.actions.executeActionRegistrationsFunction")]
    private static partial void ExecuteActionRegistrationsFunction(string vmKey);
}