namespace Vitraux.Execution.Actions;

internal interface IViewModelActionFunctionInvokerStrategy
{
    ActionRegistrationStrategy ActionRegistrationStrategy { get; }
    void Invoke(string vmKey);
}
