namespace Vitraux.Execution.ViewModelNames.Actions;

internal interface IViewModelJsActionsRepository
{
    public void AddViewModelActions(string vmKey, IEnumerable<ViewModelJsActionInfo> actions);
    public void SetViewModelInstance<TViewModel>(string vmKey, TViewModel viewModel) where TViewModel : notnull;
}
