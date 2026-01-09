namespace Vitraux.Execution;

internal interface IViewModelRepository
{
    ConfigurationBehavior ConfigurationBehavior { get; set; }
    void SetViewModelInstance<TViewModel>(string vmKey, TViewModel viewModel) where TViewModel : class;
    TViewModel GetViewModelInstance<TViewModel>(string vmKey) where TViewModel : class;
}
