namespace Vitraux.Execution;

internal interface IJsExecuteUpdateViewFunctionInvoker
{
    ValueTask Invoke(string vmKey, string json);
}
