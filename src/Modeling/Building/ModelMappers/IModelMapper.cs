using Vitraux.Modeling.Building.ElementBuilders.Collections;
using Vitraux.Modeling.Building.ElementBuilders.Values;

namespace Vitraux.Modeling.Building.ModelMappers;

public interface IModelMapper<TViewModel>
{
    IValueTargetBuilder<TViewModel, TValue> MapValue<TValue>(Func<TViewModel, TValue> func);
    ICollectionTargetBuilder<TItem, IModelMapper<TViewModel>> MapCollection<TItem>(Func<TViewModel, IEnumerable<TItem>> func);

    internal ModelMappingData Data { get; }
}