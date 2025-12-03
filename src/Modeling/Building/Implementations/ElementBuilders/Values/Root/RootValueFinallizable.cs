using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Values.Root;

internal class RootValueFinallizable<TViewModel, TValue>(
    IModelMapper<TViewModel> modelMapperWrapped,
    IRootValueMultiTargetBuilder<TViewModel, TValue> multiTargetBuilderWrapped)
    : IRootValueFinallizable<TViewModel, TValue>
{
    public IRootValueTargetBuilder<TViewModel, TValueOther> MapValue<TValueOther>(Func<TViewModel, TValueOther> func)
        => modelMapperWrapped.MapValue(func);

    public IRootCollectionTargetBuilder<TItem, TViewModel> MapCollection<TItem>(Func<TViewModel, IEnumerable<TItem>> func)
        => modelMapperWrapped.MapCollection(func);

    public IRootValueElementSelectorBuilder<TViewModel, TValue> ToElements
        => multiTargetBuilderWrapped.ToElements;

    public ModelMappingData Data
        => modelMapperWrapped.Data;

    public IRootValueCustomJsBuilder<TViewModel, TValue> ToJsFunction(string jsFunction)
        => multiTargetBuilderWrapped.ToJsFunction(jsFunction);

    public IRootActionSourceBuilder<TViewModel> MapActionAsync(Func<TViewModel, Task> action)
        => modelMapperWrapped.MapActionAsync(action);

    public IRootActionSourceBuilder<TViewModel> MapAction(Action<TViewModel> action)
        => modelMapperWrapped.MapAction(action);

    public IRootParametrizableActionSourceBuilder<TViewModel> MapActionAsync<TActionParametersBinder>() where TActionParametersBinder : class, IActionParametersBinderAsync<TViewModel>
        => modelMapperWrapped.MapActionAsync<TActionParametersBinder>();

    public IRootParametrizableActionSourceBuilder<TViewModel> MapAction<TActionParametersBinder>() where TActionParametersBinder : class, IActionParametersBinder<TViewModel>
        => modelMapperWrapped.MapAction<TActionParametersBinder>();
}