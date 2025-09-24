using Vitraux.Execution.JsInvokers.Actions;

namespace Vitraux.Execution.Actions;

internal class OnFirstViewModelActionFunctionInvokerStrategy : IViewModelActionFunctionInvokerStrategy
{
    private Action<string> _invoker;

    public OnFirstViewModelActionFunctionInvokerStrategy(IJsExecuteActionRegistrationsFunctionInvoker jsInvoker)
    {
        _invoker = (vmKey) =>
        {
            jsInvoker.Invoke(vmKey);
            _invoker = NoOp;
        };
    }

    public ActionRegistrationStrategy ActionRegistrationStrategy { get; } = ActionRegistrationStrategy.OnlyOnceOnFirstViewModelRendering;

    public void Invoke(string vmKey)
        => _invoker.Invoke(vmKey);

    private void NoOp(string _) { }
}
