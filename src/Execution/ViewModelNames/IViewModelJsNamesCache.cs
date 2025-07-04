namespace Vitraux.Execution.ViewModelNames;

internal interface IViewModelJsNamesCache<TViewModel>
{
    public string ViewModelKey { get; set; }
    public ViewModelJsNames ViewModelJsNames { get; set; }
}
