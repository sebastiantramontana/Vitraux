using Vitraux.Helpers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;
using Vitraux.Modeling.Building.Selectors.Insertion;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

internal class ElementNamesGenerator : IElementNamesGenerator
{
    public IEnumerable<ElementObjectName> Generate(string namePrefix, IEnumerable<ElementSelectorBase> selectors)
        => selectors
            .Select((selector, indexAsPostfix) => CreateElementObjectName(namePrefix, indexAsPostfix, selector))
            .RunNow();

    private static ElementObjectName CreateElementObjectName(string namePrefix, int indexAsPostfix, ElementSelectorBase selector)
    {
        var objectName = $"{namePrefix}elements{indexAsPostfix}";

        return selector is PopulatingElementSelectorBase templateSelector
            ? new PopulatingElementObjectName(objectName, $"{objectName}_appendTo", templateSelector)
            : new ElementObjectName(objectName, selector);
    }
}

internal interface ICollectionElementNamesGenerator
{
    IEnumerable<CollectionElementObjectName> GenerateInsertionNames(IEnumerable<CollectionSelector> selectors, IEnumerable<ElementObjectName> allElementObjectNames);
}

internal class CollectionElementNamesGenerator : ICollectionElementNamesGenerator
{
    public IEnumerable<CollectionElementObjectName> GenerateInsertionNames(IEnumerable<CollectionSelector> selectors, IEnumerable<ElementObjectName> allElementObjectNames)
        => selectors
            .Select((selector, indexAsPostfix) => CreateElementObjectName(selector, allElementObjectNames))
            .RunNow();

    private static CollectionElementObjectName CreateElementObjectName(CollectionSelector selector, IEnumerable<ElementObjectName> allElementObjectNames)
    {
        var objectName = GetCollectionElementName(selector, allElementObjectNames);
        return new(objectName, $"{objectName}_insertFrom", selector);
    }

    private static string GetCollectionElementName(CollectionSelector selector, IEnumerable<ElementObjectName> allElementObjectNames)
        => allElementObjectNames.Single(e => e.AssociatedSelector.Equals(selector.AppendToElementSelector)).Name;
}