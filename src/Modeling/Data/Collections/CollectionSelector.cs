using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Insertion;

namespace Vitraux.Modeling.Data.Collections;

internal record class CollectionSelector(ElementSelectorBase AppendToElementSelector) : Target<CollectionTarget>
{
    internal InsertionSelectorBase InsertionSelector { get; set; } = default!;
}
