namespace Vitraux.Execution.Building.JsViewUpdatingInvokation;

internal interface IJsInitializeNonCachedViewFunctionsInvoker
{
    void Invoke(string vmKey, string initializationJsCode, string updateViewJsCode);
}
