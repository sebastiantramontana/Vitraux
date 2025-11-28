namespace Vitraux.Execution.ViewModelNames;

internal interface IViewModelJsNamesRepositoryGeneric<TViewModel> : IViewModelJsNamesRepository
{
    public string ViewModelKey { get; set; }
    ViewModelJsNames GetNamesByViewModelType(Type vmType);
}
