using Vitraux.Execution.Actions;

namespace Vitraux;

public abstract class ActionParametersBinderBase<TViewModel> : IActionParametersBinder<TViewModel>
{
    object? IActionParametersBinderDispatch.BindActionToDispatch(object viewModel, IDictionary<string, IEnumerable<string>> parameters)
    {
        BindAction((TViewModel)viewModel, parameters);
        return null;
    }

    public abstract void BindAction(TViewModel viewModel, IDictionary<string, IEnumerable<string>> parameters);
}
