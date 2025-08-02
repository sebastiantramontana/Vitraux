using System.Diagnostics.CodeAnalysis;

namespace Vitraux.Execution.ViewModelNames;

internal class ViewModelJsNamesCacheGeneric<TViewModel>(IServiceProvider serviceProvider) : IViewModelJsNamesCacheGeneric<TViewModel>
{
    public string ViewModelKey { get; set; } = string.Empty;
    public ViewModelJsNames ViewModelJsNames { get; set; } = default!;

    [UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "IViewModelJsNamesCacheGeneric<VMType> MUST be registered by user adding the respective ModelConfiguration<VMType>")]
    public ViewModelJsNames GetNamesByViewModelType(Type vmType)
    {
        var viewModelJsNamesCacheType = typeof(IViewModelJsNamesCacheGeneric<>).MakeGenericType(vmType);
        var viewModelJsNamesCache = serviceProvider.GetService(viewModelJsNamesCacheType) as IViewModelJsNamesCache;
        return viewModelJsNamesCache!.ViewModelJsNames;
    }
}
