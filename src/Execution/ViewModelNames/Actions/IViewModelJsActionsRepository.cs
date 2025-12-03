namespace Vitraux.Execution.ViewModelNames.Actions;

internal interface IViewModelJsActionsRepository
{
    public void AddViewModelActions(string vmKey, IEnumerable<ViewModelJsActionInfo> actions);
}
