using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.Modeling.Data.Actions;

internal record class ActionTarget(ActionData Parent) : ITarget
{
    internal ElementSelectorBase Selector { get; set; } = default!;
    internal string[] Events { get; set; } = [];
}

