namespace Vitraux.Execution.ViewModelNames;

internal class ViewModelJsNamesCache<TViewModel> : IViewModelJsNamesCache<TViewModel>
{
    public string ViewModelKey { get; set; } = string.Empty;
    public ViewModelJsNames ViewModelJsNames { get; set; } = default!;
}
