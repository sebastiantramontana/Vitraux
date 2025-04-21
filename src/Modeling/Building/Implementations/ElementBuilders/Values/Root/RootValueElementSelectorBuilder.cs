using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;
using Vitraux.Modeling.Data.Selectors.Values;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Values.Root;

internal class RootValueElementSelectorBuilder<TViewModel, TValue>(
    ElementValueTarget target,
    IModelMapper<TViewModel> modelMapper,
    IRootValueMultiTargetBuilder<TViewModel, TValue> multiTargetBuilder)
    : IRootValueElementSelectorBuilder<TViewModel, TValue>
{
    public IRootValueElementPlaceBuilder<TViewModel, TValue> ById(string id)
        => SetSelectorToTarget(new ElementIdSelectorString(id));

    public IRootValueElementPlaceBuilder<TViewModel, TValue> ById(Func<TValue, string> idFunc)
        => SetSelectorToTarget(new ElementIdSelectorDelegate(idFunc));

    public IRootValueElementPlaceBuilder<TViewModel, TValue> ById(Func<TViewModel, string> idFunc)
        => SetSelectorToTarget(new ElementIdSelectorDelegate(idFunc));

    public IRootValueElementPlaceBuilder<TViewModel, TValue> ByQuery(string query)
        => SetSelectorToTarget(new ElementQuerySelectorString(query));

    public IRootValueElementPlaceBuilder<TViewModel, TValue> ByQuery(Func<TValue, string> queryFunc)
        => SetSelectorToTarget(new ElementQuerySelectorDelegate(queryFunc));

    public IRootValueElementPlaceBuilder<TViewModel, TValue> ByQuery(Func<TViewModel, string> queryFunc)
        => SetSelectorToTarget(new ElementQuerySelectorDelegate(queryFunc));


    private RootValueElementPlaceBuilder<TViewModel, TValue> SetSelectorToTarget(ElementSelectorBase elementSelector)
    {
        target.Selector = elementSelector;
        return new(target, modelMapper, multiTargetBuilder);
    }
}