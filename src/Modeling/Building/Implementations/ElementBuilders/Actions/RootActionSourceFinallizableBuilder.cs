using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionSourceFinallizableBuilder<TViewModel>(
    IRootActionSourceBuilder<TViewModel> rootActionSourceBuilder,
    IModelMapper<TViewModel> modelMapper) : IRootActionSourceFinallizableBuilder<TViewModel>
{
    public IRootActionInputSourceSelectorBuilder<TViewModel> FromInputs
        => rootActionSourceBuilder.FromInputs;

    public ModelMappingData Data
        => modelMapper.Data;

    public IRootActionSourceBuilder<TViewModel> MapAction(Func<TViewModel, IDictionary<string, IEnumerable<string>>, Task> action)
        => modelMapper.MapAction(action);

    public IRootCollectionTargetBuilder<TItem, TViewModel> MapCollection<TItem>(Func<TViewModel, IEnumerable<TItem>> func)
        => modelMapper.MapCollection(func);

    public IRootValueTargetBuilder<TViewModel, TValue> MapValue<TValue>(Func<TViewModel, TValue> func)
        => modelMapper.MapValue(func);
}
