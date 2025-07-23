namespace Vitraux.Execution.ViewModelNames;

internal class ViewModelJsNamesCacheGeneric<TViewModel>(IServiceProvider serviceProvider) : IViewModelJsNamesCacheGeneric<TViewModel>
{
    public string ViewModelKey { get; set; } = string.Empty;
    public ViewModelJsNames ViewModelJsNames { get; set; } = default!;

    public ViewModelJsNames GetNamesByViewModelType(Type vmType)
    {
        var viewModelJsNamesCacheType = typeof(IViewModelJsNamesCacheGeneric<>).MakeGenericType(vmType);
        var viewModelJsNamesCache = serviceProvider.GetService(viewModelJsNamesCacheType) as IViewModelJsNamesCache;
        return viewModelJsNamesCache!.ViewModelJsNames;
    }
}
