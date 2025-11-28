using System.Diagnostics.CodeAnalysis;

namespace Vitraux.Execution.ViewModelNames;

internal class ViewModelJsNamesRepositoryGeneric<TViewModel>(IServiceProvider serviceProvider) : IViewModelJsNamesRepositoryGeneric<TViewModel>
{
    public string ViewModelKey { get; set; } = string.Empty;
    public ViewModelJsNames ViewModelJsNames { get; set; } = default!;

    [UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "IViewModelJsNamesCacheGeneric<VMType> MUST be registered by user adding the respective ModelConfiguration<VMType>")]
    public ViewModelJsNames GetNamesByViewModelType(Type vmType)
    {
        var viewModelJsNamesRepositoryType = typeof(IViewModelJsNamesRepositoryGeneric<>).MakeGenericType(vmType);
        var viewModelJsNamesRepository = serviceProvider.GetService(viewModelJsNamesRepositoryType) as IViewModelJsNamesRepository;
        return viewModelJsNamesRepository!.ViewModelJsNames;
    }
}
