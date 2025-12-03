namespace Vitraux.Execution;

internal interface IViewModelRepository
{
    public void SetViewModelInstance<TViewModel>(string vmKey, TViewModel viewModel) where TViewModel : class;
    public TViewModel GetViewModelInstance<TViewModel>(string vmKey) where TViewModel : class;
}
