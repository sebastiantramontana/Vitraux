namespace Vitraux;

public interface IActionParametersBinderDispatch
{
    internal object? BindActionToDispatch(object viewModel, IDictionary<string, IEnumerable<string>> parameters);
}
