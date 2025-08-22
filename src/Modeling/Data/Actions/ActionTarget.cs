using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.Modeling.Data.Actions;

internal record class ActionTarget(ElementSelectorBase Selector) : IActionTarget
{
    private const string PassValueParameterName = "Value";
    private readonly ICollection<ActionSourceParameter> _parameters = [];

    internal string Event { get; set; } = default!;
    internal IEnumerable<ActionSourceParameter> Parameters => _parameters;

    internal void AddParameter(ActionSourceParameter newParameter) => _parameters.Add(newParameter);
    internal void PassValueParameter() => _parameters.Add(new(PassValueParameterName) { Selector = Selector, ElementPlace = ValuePropertyElementPlace.Instance });
}

