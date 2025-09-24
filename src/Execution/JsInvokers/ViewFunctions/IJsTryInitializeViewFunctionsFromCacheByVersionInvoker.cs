namespace Vitraux.Execution.JsInvokers.ViewFunctions;

internal interface IJsTryInitializeViewFunctionsFromCacheByVersionInvoker
{
    Task<bool> Invoke(string vmKey, string version);
}
