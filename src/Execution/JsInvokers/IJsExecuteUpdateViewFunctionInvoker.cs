namespace Vitraux.Execution.JsInvokers;

internal interface IJsExecuteUpdateViewFunctionInvoker
{
    ValueTask Invoke(string vmKey, string json);
}
