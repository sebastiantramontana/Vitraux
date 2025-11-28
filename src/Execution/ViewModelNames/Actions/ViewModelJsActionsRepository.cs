namespace Vitraux.Execution.ViewModelNames.Actions;

internal class ViewModelJsActionsRepository : IViewModelJsActionsRepository
{
    private static readonly Dictionary<ViewModelActionKey, ViewModelJsActionInfo> _vmActions = [];
    private static readonly Dictionary<string, object> _vmInstances = [];

    public void AddViewModelActions(string vmKey, IEnumerable<ViewModelJsActionInfo> actions)
    {
        foreach (var action in actions)
            _vmActions.Add(new ViewModelActionKey(vmKey, action.ActionKey), action);
    }

    public void SetViewModelInstance<TViewModel>(string vmKey, TViewModel viewModel) where TViewModel : notnull
        => _vmInstances[vmKey] = viewModel;

    internal static ViewModelJsActionInfo GetViewModelAction(string vmKey, string actionKey)
        => _vmActions.TryGetValue(new ViewModelActionKey(vmKey, actionKey), out var action)
            ? action
            : throw new InvalidOperationException($"Internal Vitraux Error: VM key {vmKey} not registered in {nameof(ViewModelJsActionsRepository)}");

    internal static object GetViewModel(string vmKey)
        => _vmInstances.TryGetValue(vmKey, out var viewModel)
        ? viewModel
        : new InvalidOperationException($"Viewmodel instance with key {vmKey} not found. Probably, IViewUpdater<TViewModel>.Update(viewModel) was never called");

    private record class ViewModelActionKey(string VMKey, string ActionKey);
}