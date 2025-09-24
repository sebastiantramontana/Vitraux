namespace Vitraux.Execution.JsInvokers.ViewFunctions;

internal interface IJsInitializeNonCachedViewFunctionsInvoker
{
    Task Invoke(string vmKey, string initializationJsCode, string updateViewJsCode);
}
