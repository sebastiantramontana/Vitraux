namespace Vitraux.Execution.ViewModelNames.Actions;

internal record class ViewModelJsActionInfo(string ActionKey, object Invokable, bool PassInputValueParameter, IEnumerable<string> ParanNames);