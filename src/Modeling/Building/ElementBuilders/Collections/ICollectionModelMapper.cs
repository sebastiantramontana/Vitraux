namespace Vitraux.Modeling.Building.ElementBuilders.Collections;

public interface ICollectionModelMapper<TItem, TEndCollectionReturn>
{
    ICollectionValueTargetBuilder<TItem, TValue, TEndCollectionReturn> MapValue<TValue>(Func<TItem, TValue> func);
    ICollectionTargetBuilder<TInnerItem, IInnerCollectionFinallizable<TItem, TEndCollectionReturn>> MapCollection<TInnerItem>(Func<TItem, IEnumerable<TInnerItem>> func);
    internal ModelMappingData Data { get; }
}
