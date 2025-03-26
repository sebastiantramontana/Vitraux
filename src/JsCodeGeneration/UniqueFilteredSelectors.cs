using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Models;

namespace Vitraux.JsCodeGeneration;

internal record class UniqueFilteredSelectors(IEnumerable<ElementSelectorBase> ElementSelectors, IEnumerable<CollectionSelector> CollectionSelectors);
