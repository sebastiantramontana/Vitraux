using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Selectors.Elements;

namespace Vitraux.JsCodeGeneration;

internal record class UniqueFilteredSelectors(IEnumerable<ElementSelectorBase> ElementSelectors, IEnumerable<CollectionElementTarget> CollectionSelectors);
