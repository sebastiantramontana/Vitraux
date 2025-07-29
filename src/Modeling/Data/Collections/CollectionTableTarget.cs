using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.Modeling.Data.Collections;

internal record class CollectionTableTarget(ElementSelectorBase AppendToElementSelector) : CollectionElementTarget(AppendToElementSelector)
{
    internal int TBodyIndex { get; set; } = 0;
}
