namespace Vitraux.Execution.JsInvokers.Actions.Registration;

internal interface IJsInitializeNewActionsFunctionToCacheByVersionInvoker
{
    void Invoke(string vmKey, string version, string actionsJsCode, ActionRegistrationStrategy actionRegistrationStrategy);
}