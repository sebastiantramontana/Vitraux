using System.Runtime.InteropServices.JavaScript;
using Vitraux.Execution.JsInvokers.Actions.Registration;

namespace Vitraux.Execution.JsInvokers.Actions;

internal partial class JsExecuteActionRegistrationsFunctionInvoker : IJsExecuteActionRegistrationsFunctionInvoker
{
    public void Invoke(string vmKey)
       => ExecuteActionRegistrationsFunction(vmKey);

    [JSImport("globalThis.vitraux.actions.registration.executeActionRegistrationsFunction")]
    private static partial void ExecuteActionRegistrationsFunction(string vmKey);
}