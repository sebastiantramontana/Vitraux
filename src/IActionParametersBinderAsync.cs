namespace Vitraux;

public interface IActionParametersBinderAsync<TViewModel>
{
    Task BindActionAsync(TViewModel viewModel, IDictionary<string, IEnumerable<string>> parameters);
}
