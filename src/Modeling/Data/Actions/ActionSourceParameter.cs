using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.Modeling.Data.Actions;

internal record class ActionSourceParameter(string ParamName)
{
    internal ElementSelectorBase Selector { get; set; } = default!;
    internal ElementPlace ElementPlace { get; set; } = default!;
}

