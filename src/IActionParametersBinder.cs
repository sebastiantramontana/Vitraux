using Vitraux.Execution.Actions;

namespace Vitraux;
public interface IActionParametersBinder<TViewModel>: IActionParametersBinderDispatch
{
    void BindAction(TViewModel viewModel, IDictionary<string, IEnumerable<string>> parameters);
}
