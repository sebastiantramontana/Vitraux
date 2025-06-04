namespace Vitraux.Execution.Building.JsViewUpdatingInvokation;

internal interface IJsCreateUpdateFunctionInvoker
{
    ValueTask ExecuteVersionCached(string vmKey, string version, string jsCode);
    ValueTask ExecuteNoCache(string vmKey, string jsCode);
}
