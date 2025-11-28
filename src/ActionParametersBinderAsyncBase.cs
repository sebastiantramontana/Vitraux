namespace Vitraux;

public abstract class ActionParametersBinderAsyncBase<TViewModel> : IActionParametersBinderAsync<TViewModel>
{
    object? IActionParametersBinderDispatch.BindActionToDispatch(object viewModel, IDictionary<string, IEnumerable<string>> parameters) 
        => BindActionAsync((TViewModel)viewModel, parameters);

    public abstract Task BindActionAsync(TViewModel viewModel, IDictionary<string, IEnumerable<string>> parameters);
}