namespace Vitraux.Execution.JsInvokers;

internal interface IJsInitializeNewViewFunctionsToCacheByVersionInvoker
{
    ValueTask Invoke(string vmKey, string version, string initializationJsCode, string updateViewJsCode);
}
