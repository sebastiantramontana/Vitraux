namespace Vitraux.Execution.JsInvokers;

internal interface IJsTryInitializeViewFunctionsFromCacheByVersionInvoker
{
    ValueTask<bool> Invoke(string vmKey, string version);
}
