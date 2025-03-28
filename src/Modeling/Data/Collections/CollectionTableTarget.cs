using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.Modeling.Data.Collections;

internal record class CollectionTableTarget(ElementSelectorBase AppendToElementSelector) : CollectionElementTarget(AppendToElementSelector);
