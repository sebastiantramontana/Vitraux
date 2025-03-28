using Vitraux.Modeling.Data.Selectors.Elements;
using Vitraux.Modeling.Data.Selectors.Insertion;

namespace Vitraux.Modeling.Data.Collections;

internal record class CollectionSelector(ElementSelectorBase AppendToElementSelector) : Target<CollectionTarget>
{
    internal InsertionSelectorBase InsertionSelector { get; set; } = default!;
}
