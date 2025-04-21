using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root.Insertions;
using Vitraux.Modeling.Data.Selectors.Values;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Values.Root.Insertions;

internal class RootChildrenSelectorBuilder<TViewModel, TValue>(
    ElementValueTarget target,
    IModelMapper<TViewModel> modelMapper,
    IRootValueMultiTargetBuilder<TViewModel, TValue> multiTargetBuilder)
    : IRootChildrenSelectorBuilder<TViewModel, TValue>
{
    public IRootChildrenPlaceBuilder<TViewModel, TValue> ByQuery(string query)
        => SetChildQuerySelector(new ElementQuerySelectorString(query));

    public IRootChildrenPlaceBuilder<TViewModel, TValue> ByQuery(Func<TValue, string> queryFunc)
        => SetChildQuerySelector(new ElementQuerySelectorDelegate(queryFunc));

    public IRootChildrenPlaceBuilder<TViewModel, TValue> ByQuery(Func<TViewModel, string> queryFunc)
        => SetChildQuerySelector(new ElementQuerySelectorDelegate(queryFunc));

    private RootChildrenPlaceBuilder<TViewModel, TValue> SetChildQuerySelector(ElementQuerySelectorBase elementQuerySelector)
    {
        target.Insertion!.TargetChildElement = elementQuerySelector;
        return new RootChildrenPlaceBuilder<TViewModel, TValue>(target, modelMapper, multiTargetBuilder);
    }
}
