using Vitraux.Helpers;
using Vitraux.Modeling.Data;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Selectors;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration;

internal class UniqueSelectorsFilter : IUniqueSelectorsFilter
{
    public IEnumerable<SelectorBase> FilterDistinct(ModelMappingData modelMappingData) 
        => GetValueSelectors(modelMappingData)
            .Concat(GetCollectionSelectors(modelMappingData))
            .Distinct()
            .RunNow();

    private static IEnumerable<SelectorBase> GetValueSelectors(ModelMappingData modelMappingData)
        => modelMappingData
            .Values
            .SelectMany(GetTargetsFromData<IValueTarget, ElementValueTarget>)
            .SelectMany(GetSelectorsFromElementTarget)
            .Distinct()
            .RunNow();

    private static IEnumerable<SelectorBase> GetCollectionSelectors(ModelMappingData modelMappingData)
        => modelMappingData
            .Collections
            .SelectMany(GetTargetsFromData<ICollectionTarget, CollectionElementTarget>)
            .SelectMany(GetSelectorsFromCollectionElementTarget)
            .Distinct()
            .RunNow();

    private static IEnumerable<ITargetReturn> GetTargetsFromData<TTargetBase, ITargetReturn>(DelegateDataBase<TTargetBase> data)
        where TTargetBase : ITarget
        where ITargetReturn : TTargetBase
        => data
            .Targets
            .OfType<ITargetReturn>()
            .RunNow();

    private static IEnumerable<SelectorBase> GetSelectorsFromCollectionElementTarget(CollectionElementTarget collectionElementTarget)
        => [collectionElementTarget.AppendToElementSelector, collectionElementTarget.InsertionSelector];


    private static IEnumerable<SelectorBase> GetSelectorsFromElementTarget(ElementValueTarget elementTarget)
        => [elementTarget.Selector, .. TotSafeEnumerable(elementTarget.Insertion)];

    private static IEnumerable<T> TotSafeEnumerable<T>(T? obj)
        => obj is not null ? [obj] : [];
}
