namespace Vitraux.Execution.ViewModelNames;

internal class ViewModelJsNamesCache<TViewModel>(IServiceProvider serviceProvider) : IViewModelJsNamesCacheGeneric<TViewModel>
{
    public string ViewModelKey { get; set; } = string.Empty;
    public ViewModelJsNames ViewModelJsNames { get; set; } = default!;

    public ViewModelJsNames GetNamesByViewModelType(Type vmType)
    {
        var viewModelJsNamesCacheType = typeof(IViewModelJsNamesCacheGeneric<>).MakeGenericType(vmType);
        var ViewModelJsNamesCache = serviceProvider.GetService(viewModelJsNamesCacheType) as IViewModelJsNamesCache;
        return ViewModelJsNamesCache!.ViewModelJsNames;
    }
}
