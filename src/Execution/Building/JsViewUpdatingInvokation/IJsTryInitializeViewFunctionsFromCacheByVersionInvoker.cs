namespace Vitraux.Execution.Building.JsViewUpdatingInvokation;

internal interface IJsTryInitializeViewFunctionsFromCacheByVersionInvoker
{
    ValueTask<bool> Invoke(string vmKey, string version);
}
