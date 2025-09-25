namespace Vitraux.Execution.JsInvokers.Actions.Registration;

internal interface IJsTryInitializeActionsFunctionFromCacheByVersionInvoker
{
    bool Invoke(string vmKey, string version);
}