using Vitraux.Helpers;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Models;

namespace Vitraux.JsCodeGeneration;

internal class UniqueSelectorsFilter : IUniqueSelectorsFilter
{
    public UniqueFilteredSelectors FilterDistinct(IModelMappingData modelMappingData)
    {
        var elementSelectors = GetElementSelectors(modelMappingData);
        var collectionSelectors = GetCollectionSelectorsDistinctByInsertion(modelMappingData);

        return new UniqueFilteredSelectors(elementSelectors, collectionSelectors);
    }

    private static IEnumerable<ElementSelectorBase> GetElementSelectors(IModelMappingData modelMappingData)
        => modelMappingData
            .Values
            .SelectMany(v => v.TargetElements.Select(te => te.Selector))
            .Concat(modelMappingData.CollectionElements.Select(c => c.CollectionSelector.AppendToElementSelector))
            .Distinct()
            .RunNow();

    private static IEnumerable<CollectionSelector> GetCollectionSelectorsDistinctByInsertion(IModelMappingData modelMappingData)
        => modelMappingData
            .CollectionElements
            .Select(c => c.CollectionSelector)
            .DistinctBy(c => c.InsertionSelector)
            .RunNow();
}

internal record UniqueFilteredSelectors(IEnumerable<ElementSelectorBase> ElementSelectors, IEnumerable<CollectionSelector> CollectionSelectors);
