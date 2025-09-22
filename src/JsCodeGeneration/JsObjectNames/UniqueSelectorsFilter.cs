using Vitraux.Modeling.Data;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Selectors;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.JsObjectNames;

internal class UniqueSelectorsFilter : IUniqueSelectorsFilter
{
    public IEnumerable<SelectorBase> FilterDistinct(ModelMappingData modelMappingData)
        => GetValueSelectors(modelMappingData)
            .Concat(GetCollectionSelectors(modelMappingData))
            .Distinct();

    private static IEnumerable<SelectorBase> GetValueSelectors(ModelMappingData modelMappingData)
        => GetSelectors<IValueTarget, ElementValueTarget>(modelMappingData.Values, GetSelectorsFromElementTarget);

    private static IEnumerable<SelectorBase> GetCollectionSelectors(ModelMappingData modelMappingData)
        => GetSelectors<ICollectionTarget, CollectionElementTarget>(modelMappingData.Collections, GetSelectorsFromCollectionElementTarget);

    private static IEnumerable<SelectorBase> GetSelectors<TTargetBase, TTargetReturn>(IEnumerable<DelegateDataBase<TTargetBase>> data, Func<TTargetReturn, IEnumerable<SelectorBase>> selectorFunc)
        where TTargetBase : ITarget
        where TTargetReturn : TTargetBase
        => data
            .SelectMany(GetTargetsFromData<TTargetBase, TTargetReturn>)
            .SelectMany(selectorFunc);

    private static IEnumerable<TTargetReturn> GetTargetsFromData<TTargetBase, TTargetReturn>(DelegateDataBase<TTargetBase> data)
        where TTargetBase : ITarget
        where TTargetReturn : TTargetBase
        => data
            .Targets
            .OfType<TTargetReturn>();

    private static IEnumerable<SelectorBase> GetSelectorsFromCollectionElementTarget(CollectionElementTarget collectionElementTarget)
        => [collectionElementTarget.AppendToElementSelector, collectionElementTarget.InsertionSelector];

    private static IEnumerable<SelectorBase> GetSelectorsFromElementTarget(ElementValueTarget elementTarget)
        => [elementTarget.Selector, .. TotSafeEnumerable(elementTarget.Insertion)];

    private static IEnumerable<T> TotSafeEnumerable<T>(T? obj)
        => obj is not null ? [obj] : [];
}
