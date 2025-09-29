namespace Vitraux;

public interface IActionParametersBinderAsync<TViewModel>
{
    Task BindAction(TViewModel viewModel, IDictionary<string, IEnumerable<string>> parameters);
}
