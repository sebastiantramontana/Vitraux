
namespace Vitraux.Execution.JsInvokers.Actions;

internal interface IJsTryInitializeActionsFunctionFromCacheByVersionInvoker
{
    bool Invoke(string vmKey, string version);
}