using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.Modeling.Data.Values;

internal record class ElementTarget(ElementSelectorBase Selector) : ValueTarget
{
    internal ElementPlace Place { get; set; } = default!;
}
