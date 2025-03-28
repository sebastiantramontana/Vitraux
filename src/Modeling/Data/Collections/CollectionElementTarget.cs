using Vitraux.Modeling.Data.Selectors.Collections;
using Vitraux.Modeling.Data.Selectors.Elements;

namespace Vitraux.Modeling.Data.Collections;

internal record class CollectionElementTarget(ElementSelectorBase AppendToElementSelector) : CollectionTarget
{
    internal InsertionSelectorBase InsertionSelector { get; set; } = default!;
}
