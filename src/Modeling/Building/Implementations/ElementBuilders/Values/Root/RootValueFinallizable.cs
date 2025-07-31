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
}