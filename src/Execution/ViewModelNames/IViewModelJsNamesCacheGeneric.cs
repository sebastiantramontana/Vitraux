namespace Vitraux.Execution.ViewModelNames;

internal interface IViewModelJsNamesCacheGeneric<TViewModel> : IViewModelJsNamesCache
{
    public string ViewModelKey { get; set; }
    ViewModelJsNames GetNamesByViewModelType(Type vmType);
}
