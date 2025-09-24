namespace Vitraux.Execution.Actions;

internal class ViewModelActionFunctionInvokerContext(IEnumerable<IViewModelActionFunctionInvokerStrategy> strategies) : IViewModelActionFunctionInvokerContext
{
    public IViewModelActionFunctionInvokerStrategy GetStrategy(ActionRegistrationStrategy actionRegistrationStrategy)
        => strategies.Single(s => s.ActionRegistrationStrategy == actionRegistrationStrategy);
}
