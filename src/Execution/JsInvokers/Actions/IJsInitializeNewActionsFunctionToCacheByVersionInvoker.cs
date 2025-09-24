namespace Vitraux.Execution.JsInvokers.Actions;

internal interface IJsInitializeNewActionsFunctionToCacheByVersionInvoker
{
    void Invoke(string vmKey, string version, string actionsJsCode, ActionRegistrationStrategy actionRegistrationStrategy);
}