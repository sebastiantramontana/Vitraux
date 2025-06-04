namespace Vitraux.Execution.Building.JsViewUpdatingInvokation;

internal interface IJsInitializationInvoker
{
    ValueTask Execute(string jsCode);
}
