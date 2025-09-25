using Vitraux.Execution.JsInvokers.Actions.Registration;

namespace Vitraux.Execution.Actions;

internal class AlwaysViewModelActionFunctionInvokerStrategy(IJsExecuteActionRegistrationsFunctionInvoker jsInvoker) : IViewModelActionFunctionInvokerStrategy
{
    public ActionRegistrationStrategy ActionRegistrationStrategy { get; } = ActionRegistrationStrategy.AlwaysOnViewModelRendering;

    public void Invoke(string vmKey)
        => jsInvoker.Invoke(vmKey);
}
