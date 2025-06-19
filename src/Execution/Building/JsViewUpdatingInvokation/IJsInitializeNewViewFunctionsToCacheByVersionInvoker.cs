namespace Vitraux.Execution.Building.JsViewUpdatingInvokation;

internal interface IJsInitializeNewViewFunctionsToCacheByVersionInvoker
{
    void Invoke(string vmKey, string version, string initializationJsCode, string updateViewJsCode);
}
