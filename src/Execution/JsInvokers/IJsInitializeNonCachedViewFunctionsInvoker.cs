namespace Vitraux.Execution.JsInvokers;

internal interface IJsInitializeNonCachedViewFunctionsInvoker
{
    void Invoke(string vmKey, string initializationJsCode, string updateViewJsCode);
}
