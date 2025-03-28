using Vitraux.Modeling.Data.Selectors.Elements;

namespace Vitraux.Modeling.Data.Values;

internal record class ElementTarget(ElementSelectorBase Selector) : ValueTarget
{
    internal ElementPlace Place { get; set; } = default!;
}
