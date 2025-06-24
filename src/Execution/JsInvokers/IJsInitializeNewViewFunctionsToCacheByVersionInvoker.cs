namespace Vitraux.Execution.JsInvokers;

internal interface IJsInitializeNewViewFunctionsToCacheByVersionInvoker
{
    void Invoke(string vmKey, string version, string initializationJsCode, string updateViewJsCode);
}
