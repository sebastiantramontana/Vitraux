using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.Modeling.Data.Values;

internal record class ElementTarget(ElementSelectorBase Selector) : ValueTarget
{
    internal ElementPlace Place { get; set; } = default!;
}
