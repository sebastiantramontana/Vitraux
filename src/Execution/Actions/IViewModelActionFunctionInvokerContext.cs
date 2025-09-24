namespace Vitraux.Execution.Actions;

internal interface IViewModelActionFunctionInvokerContext
{
    IViewModelActionFunctionInvokerStrategy GetStrategy(ActionRegistrationStrategy actionRegistrationStrategy);
}
