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

    public IRootValueElementPlaceBuilder<TViewModel, TValue> ByQuery(string query)
        => SetSelectorToTarget(new ElementQuerySelectorString(query));

    private RootValueElementPlaceBuilder<TViewModel, TValue> SetSelectorToTarget(ElementSelectorBase elementSelector)
    {
        target.Selector = elementSelector;
        return new(target, modelMapper, multiTargetBuilder);
    }
}