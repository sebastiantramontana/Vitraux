namespace Vitraux.Execution.JsInvokers;

internal interface IJsInitializeNonCachedViewFunctionsInvoker
{
    ValueTask Invoke(string vmKey, string initializationJsCode, string updateViewJsCode);
}
