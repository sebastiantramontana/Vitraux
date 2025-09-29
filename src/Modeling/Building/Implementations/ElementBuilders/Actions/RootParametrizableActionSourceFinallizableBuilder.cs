using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootParametrizableActionSourceFinallizableBuilder<TViewModel>(
    IRootParametrizableActionSourceBuilder<TViewModel> rootActionSourceBuilder,
    IModelMapper<TViewModel> modelMapper) : IRootParametrizableActionSourceFinallizableBuilder<TViewModel>
{
    public IRootParametrizableActionInputSourceSelectorBuilder<TViewModel> FromInputs
        => rootActionSourceBuilder.FromInputs;

    public ModelMappingData Data
        => modelMapper.Data;

    public IRootActionSourceBuilder<TViewModel> MapAction(Func<TViewModel, Task> action)
        => modelMapper.MapAction(action);
    public IRootActionSourceBuilder<TViewModel> MapAction(Action<TViewModel> action)
        => modelMapper.MapAction(action);
    public IRootParametrizableActionSourceBuilder<TViewModel> MapAction(IActionParametersBinderAsync<TViewModel> binder)
        => modelMapper.MapAction(binder);
    public IRootParametrizableActionSourceBuilder<TViewModel> MapAction(IActionParametersBinder<TViewModel> binder)
        => modelMapper.MapAction(binder);
    public IRootCollectionTargetBuilder<TItem, TViewModel> MapCollection<TItem>(Func<TViewModel, IEnumerable<TItem>> func)
        => modelMapper.MapCollection(func);
    public IRootValueTargetBuilder<TViewModel, TValue> MapValue<TValue>(Func<TViewModel, TValue> func)
        => modelMapper.MapValue(func);
}
