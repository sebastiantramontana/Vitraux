namespace Vitraux.Execution.JsInvokers.ViewFunctions;

internal interface IJsInitializeNewViewFunctionsToCacheByVersionInvoker
{
    Task Invoke(string vmKey, string version, string initializationJsCode, string updateViewJsCode);
}
