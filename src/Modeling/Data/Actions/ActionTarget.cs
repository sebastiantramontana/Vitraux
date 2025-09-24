using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.Modeling.Data.Actions;

internal record class ActionTarget : ITarget
{
    private const string InputValueParameterName = "Value";
    private readonly ICollection<ActionSourceParameter> _parameters = [];

    internal ElementSelectorBase Selector { get; set; } = default!;
    internal string Event { get; set; } = default!;
    internal IEnumerable<ActionSourceParameter> Parameters => _parameters;

    internal void AddParameter(ActionSourceParameter newParameter) => _parameters.Add(newParameter);
    internal void AddInputValueParameter() => _parameters.Add(new(InputValueParameterName) { Selector = Selector, ElementPlace = ValuePropertyElementPlace.Instance });
}

