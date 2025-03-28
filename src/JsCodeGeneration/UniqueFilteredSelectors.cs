using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration;

internal record class UniqueFilteredSelectors(IEnumerable<ElementSelectorBase> ElementSelectors, IEnumerable<CollectionElementTarget> CollectionSelectors);
