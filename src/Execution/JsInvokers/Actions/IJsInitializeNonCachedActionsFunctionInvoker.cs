namespace Vitraux.Execution.JsInvokers.Actions;

internal interface IJsInitializeNonCachedActionsFunctionInvoker
{
    void Invoke(string vmKey, string actionsJsCode, ActionRegistrationStrategy actionRegistrationStrategy);
}