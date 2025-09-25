using System.Runtime.InteropServices.JavaScript;
using Vitraux.Execution.JsInvokers.Actions.Registration;

namespace Vitraux.Execution.JsInvokers.Actions;

internal partial class JsInitializeNewActionsFunctionToCacheByVersionInvoker : IJsInitializeNewActionsFunctionToCacheByVersionInvoker
{
    public void Invoke(string vmKey, string version, string actionsJsCode, ActionRegistrationStrategy actionRegistrationStrategy)
       => InitializeNewActionsFunctionToCacheByVersion(vmKey, version, actionsJsCode, actionRegistrationStrategy.ToString());

    [JSImport("globalThis.vitraux.actions.registration.initializeNewActionsFunctionToCacheByVersion")]
    private static partial void InitializeNewActionsFunctionToCacheByVersion(string vmKey, string version, string actionsJsCode, string actionRegistrationStrategy);
}
