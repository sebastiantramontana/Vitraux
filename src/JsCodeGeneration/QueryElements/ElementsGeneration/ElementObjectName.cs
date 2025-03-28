using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

internal record class ElementObjectName(string Name, ElementSelectorBase AssociatedSelector);

internal record class CollectionElementObjectName(string Name, string InsertionElementName, CollectionSelector AssociatedCollectionSelector);
