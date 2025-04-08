using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;

namespace Vitraux;

public interface IModelMapper<TViewModel>
{
    IRootValueTargetBuilder<TViewModel, TValue> MapValue<TValue>(Func<TViewModel, TValue> func);
    ICollectionTargetBuilder<TItem, IModelMapper<TViewModel>> MapCollection<TItem>(Func<TViewModel, IEnumerable<TItem>> func);
    ModelMappingData Data { get; }
}