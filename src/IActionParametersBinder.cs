namespace Vitraux;
public interface IActionParametersBinder<TViewModel>
{
    void BindAction(TViewModel viewModel, IDictionary<string, IEnumerable<string>> parameters);
}
