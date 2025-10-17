using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;

namespace Vitraux;

public interface IModelMapper<TViewModel>
{
    IRootValueTargetBuilder<TViewModel, TValue> MapValue<TValue>(Func<TViewModel, TValue> func);
    IRootCollectionTargetBuilder<TItem, TViewModel> MapCollection<TItem>(Func<TViewModel, IEnumerable<TItem>> func);
    IRootActionSourceBuilder<TViewModel> MapActionAsync(Func<TViewModel, Task> action);
    IRootActionSourceBuilder<TViewModel> MapAction(Action<TViewModel> action);
    IRootParametrizableActionSourceBuilder<TViewModel> MapAction();
    IRootParametrizableActionSourceBuilder<TViewModel> MapActionAsync();
    IRootParametrizableActionSourceBuilder<TViewModel> MapAction<TActionParametersBinder>() where TActionParametersBinder : class, IActionParametersBinder<TViewModel>;
    IRootParametrizableActionSourceBuilder<TViewModel> MapActionAsync<TActionParametersBinder>() where TActionParametersBinder : class, IActionParametersBinderAsync<TViewModel>;
    ModelMappingData Data { get; }
}