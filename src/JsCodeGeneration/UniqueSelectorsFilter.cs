using Vitraux.Helpers;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration;

internal class UniqueSelectorsFilter : IUniqueSelectorsFilter
{
    public UniqueFilteredSelectors FilterDistinct(ModelMappingData modelMappingData)
    {
        var elementSelectors = GetElementSelectors(modelMappingData);
        var collectionSelectors = GetCollectionSelectorsDistinctByInsertion(modelMappingData);

        return new UniqueFilteredSelectors(elementSelectors, collectionSelectors);
    }

    private static IEnumerable<ElementSelectorBase> GetElementSelectors(ModelMappingData modelMappingData)
        => modelMappingData
            .Values
            .SelectMany(v => v.TargetElements.Select(te => te.Selector))
            .Concat(modelMappingData.CollectionElements.Select(c => c.CollectionSelector.AppendToElementSelector))
            .Distinct()
            .RunNow();

    private static IEnumerable<CollectionElementTarget> GetCollectionSelectorsDistinctByInsertion(ModelMappingData modelMappingData)
        => modelMappingData
            .CollectionElements
            .Select(c => c.CollectionSelector)
            .DistinctBy(c => c.InsertionSelector)
            .RunNow();
}
