namespace Vitraux.Modeling.Data.Actions;

internal record class ActionData(Delegate DataFunc, bool IsAsync, string ActionKey) : DelegateDataBase<ActionTarget>(DataFunc)
{
    private readonly ICollection<ActionParameter> _parameters = [];

    internal bool PassInputValueParameter { get; set; } = false;
    internal IEnumerable<ActionParameter> Parameters => _parameters;

    internal void AddParameter(ActionParameter newParameter) => _parameters.Add(newParameter);
}