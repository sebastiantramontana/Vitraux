using Vitraux.Modeling.Data.Selectors.Elements;

namespace Vitraux.Modeling.Data.Collections;

internal record class CollectionTableTarget(ElementSelectorBase AppendToElementSelector) : CollectionElementTarget(AppendToElementSelector);
