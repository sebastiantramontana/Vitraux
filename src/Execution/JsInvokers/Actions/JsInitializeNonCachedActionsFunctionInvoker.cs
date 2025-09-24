using System.Runtime.InteropServices.JavaScript;

namespace Vitraux.Execution.JsInvokers.Actions;

internal partial class JsInitializeNonCachedActionsFunctionInvoker : IJsInitializeNonCachedActionsFunctionInvoker
{
    public void Invoke(string vmKey, string actionsJsCode, ActionRegistrationStrategy actionRegistrationStrategy)
       => InitializeNonCachedActionsFunction(vmKey, actionsJsCode, actionRegistrationStrategy.ToString());

    [JSImport("globalThis.vitraux.actions.initializeNonCachedActionsFunction")]
    private static partial void InitializeNonCachedActionsFunction(string vmKey, string actionsJsCode, string actionRegistrationStrategy);
}
