namespace Vitraux.Execution.JsInvokers.Actions.Registration;

internal interface IJsInitializeNonCachedActionsFunctionInvoker
{
    void Invoke(string vmKey, string actionsJsCode, ActionRegistrationStrategy actionRegistrationStrategy);
}