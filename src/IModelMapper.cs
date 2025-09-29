using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;

namespace Vitraux;

public interface IModelMapper<TViewModel>
{
    IRootValueTargetBuilder<TViewModel, TValue> MapValue<TValue>(Func<TViewModel, TValue> func);
    IRootCollectionTargetBuilder<TItem, TViewModel> MapCollection<TItem>(Func<TViewModel, IEnumerable<TItem>> func);
    IRootActionSourceBuilder<TViewModel> MapAction(Func<TViewModel, Task> action);
    IRootActionSourceBuilder<TViewModel> MapAction(Action<TViewModel> action);
    IRootParametrizableActionSourceBuilder<TViewModel> MapAction(IActionParametersBinder<TViewModel> binder);
    IRootParametrizableActionSourceBuilder<TViewModel> MapAction(IActionParametersBinderAsync<TViewModel> binder);
    ModelMappingData Data { get; }
}