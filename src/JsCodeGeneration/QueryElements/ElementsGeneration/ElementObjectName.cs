using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Models;

namespace Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

internal record class ElementObjectName(string Name, ElementSelectorBase AssociatedSelector);

internal record class CollectionElementObjectName(string Name, string InsertionElementName, CollectionSelector AssociatedCollectionSelector);
