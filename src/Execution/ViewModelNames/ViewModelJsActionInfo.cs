namespace Vitraux.Execution.ViewModelNames;

internal record class ViewModelJsActionInfo(string ActionKey, object Invokable, bool PassInputValueParameter, IEnumerable<string> ParanNames);