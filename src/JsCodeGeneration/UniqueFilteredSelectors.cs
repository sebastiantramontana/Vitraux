using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.JsCodeGeneration;

internal record class UniqueFilteredSelectors(IEnumerable<ElementSelectorBase> ElementSelectors, IEnumerable<CollectionSelector> CollectionSelectors);
