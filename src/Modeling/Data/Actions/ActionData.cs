namespace Vitraux.Modeling.Data.Actions;

internal record class ActionData(object Invokable, bool IsAsync, string ActionKey)
{
    private readonly ICollection<ActionParameter> _parameters = [];
    private readonly ICollection<ActionTarget> _targets = [];

    internal bool PassInputValueParameter { get; set; } = false;
    internal IEnumerable<ActionParameter> Parameters => _parameters;
    internal IEnumerable<ActionTarget> Targets => _targets;

    internal void AddParameter(ActionParameter newParameter)
        => _parameters.Add(newParameter);

    internal void AddTarget(ActionTarget target)
        => _targets.Add(target);
}