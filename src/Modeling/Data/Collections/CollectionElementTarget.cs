using Vitraux.Modeling.Data.Selectors.Collections;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.Modeling.Data.Collections;

internal record class CollectionElementTarget(ElementSelectorBase AppendToElementSelector) : ICollectionTarget
{
    internal InsertionSelectorBase InsertionSelector { get; set; } = default!;
    internal ModelMappingData Data { get; } = new ModelMappingData();
}
