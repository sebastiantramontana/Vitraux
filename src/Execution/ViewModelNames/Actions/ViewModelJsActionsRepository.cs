namespace Vitraux.Execution.ViewModelNames.Actions;

internal class ViewModelJsActionsRepository : IViewModelJsActionsRepository
{
    private static readonly Dictionary<ViewModelActionKey, ViewModelJsActionInfo> _vmActions = [];

    public void AddViewModelActions(string vmKey, IEnumerable<ViewModelJsActionInfo> actions)
    {
        foreach (var action in actions)
            _vmActions.Add(new ViewModelActionKey(vmKey, action.ActionKey), action);
    }

    internal static ViewModelJsActionInfo GetViewModelActionInfo(string vmKey, string actionKey)
        => _vmActions.TryGetValue(new ViewModelActionKey(vmKey, actionKey), out var action)
            ? action
            : throw new InvalidOperationException($"Internal Vitraux Error: VM key {vmKey} not registered in {nameof(ViewModelJsActionsRepository)}");

    private record class ViewModelActionKey(string VMKey, string ActionKey);
}