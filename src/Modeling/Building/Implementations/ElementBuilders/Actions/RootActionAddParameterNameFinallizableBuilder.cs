using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class RootActionAddParameterNameFinallizableBuilder<TViewModel>(
    IRootActionAddParameterNameBuilder<TViewModel> rootActionParameterNameBuilder,
    IModelMapper<TViewModel> modelMapper)
    : IRootActionAddParameterNameFinallizableBuilder<TViewModel>
{
    public IRootActionAddParametersFromBuilder<TViewModel> AddParameter(string paramName)
        => rootActionParameterNameBuilder.AddParameter(paramName);
    public ModelMappingData Data
        => modelMapper.Data;
    public IRootActionSourceBuilder<TViewModel> MapActionAsync(Func<TViewModel, Task> action)
        => modelMapper.MapActionAsync(action);
    public IRootActionSourceBuilder<TViewModel> MapAction(Action<TViewModel> action)
        => modelMapper.MapAction(action);
    public IRootParametrizableActionSourceBuilder<TViewModel> MapActionAsync<TActionParametersBinder>() where TActionParametersBinder : class, IActionParametersBinderAsync<TViewModel>
        => modelMapper.MapActionAsync<TActionParametersBinder>();
    public IRootParametrizableActionSourceBuilder<TViewModel> MapAction<TActionParametersBinder>() where TActionParametersBinder : class, IActionParametersBinder<TViewModel>
        => modelMapper.MapAction<TActionParametersBinder>();
    public IRootParametrizableActionSourceBuilder<TViewModel> MapActionAsync()
        => modelMapper.MapActionAsync();
    public IRootParametrizableActionSourceBuilder<TViewModel> MapAction()
        => modelMapper.MapAction();
    public IRootCollectionTargetBuilder<TItem, TViewModel> MapCollection<TItem>(Func<TViewModel, IEnumerable<TItem>> func)
        => modelMapper.MapCollection(func);
    public IRootValueTargetBuilder<TViewModel, TValue> MapValue<TValue>(Func<TViewModel, TValue> func)
        => modelMapper.MapValue(func);
}