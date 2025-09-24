namespace Vitraux.Execution.JsInvokers.ViewFunctions;

internal interface IJsExecuteUpdateViewFunctionInvoker
{
    Task Invoke(string vmKey, string json);
}
