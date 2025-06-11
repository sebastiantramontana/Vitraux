namespace Vitraux.Execution.Building.JsViewUpdatingInvokation;

internal interface IJsCreateUpdateFunctionInvoker
{
    ValueTask InvokeVersionCached(string vmKey, string version, string jsCode);
    ValueTask InvokeNoCache(string vmKey, string jsCode);
}
