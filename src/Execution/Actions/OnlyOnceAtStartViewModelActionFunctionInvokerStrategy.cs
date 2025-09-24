namespace Vitraux.Execution.Actions;

internal class OnlyOnceAtStartViewModelActionFunctionInvokerStrategy : IViewModelActionFunctionInvokerStrategy
{
    public ActionRegistrationStrategy ActionRegistrationStrategy { get; } = ActionRegistrationStrategy.OnlyOnceAtStart;

    public void Invoke(string _) => NoOp();
    private static void NoOp() { }
}
