using Vitraux.Execution.Actions;

namespace Vitraux;

public interface IActionParametersBinderAsync<TViewModel> : IActionParametersBinderDispatch
{
    Task BindActionAsync(TViewModel viewModel, IDictionary<string, IEnumerable<string>> parameters);
}
