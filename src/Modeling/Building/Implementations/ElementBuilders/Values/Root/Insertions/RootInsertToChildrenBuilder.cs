using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root.Insertions;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Values.Root.Insertions;

internal class RootInsertToChildrenBuilder<TViewModel, TValue>(
    ElementTarget target,
    IModelMapper<TViewModel> modelMapper,
    IRootValueMultiTargetBuilder<TViewModel, TValue> multiTargetBuilder)
    : IRootInsertToChildrenBuilder<TViewModel, TValue>
{
    public IRootChildrenSelectorBuilder<TViewModel, TValue> ToChildren
        => new RootChildrenSelectorBuilder<TViewModel, TValue>(target, modelMapper, multiTargetBuilder);
}
