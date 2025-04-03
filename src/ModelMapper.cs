using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Values.Root;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Values;

namespace Vitraux;

internal class ModelMapper<TViewModel> : IModelMapper<TViewModel>
{
    private readonly List<ValueData> _values = [];
    private readonly List<CollectionData> _collections = [];

    public IRootValueTargetBuilder<TViewModel, TValue> MapValue<TValue>(Func<TViewModel, TValue> func)
    {
        var newValue = new ValueData(func);
        _values.Add(newValue);

        return new RootValueTargetBuilder<TViewModel, TValue>(newValue, this);
    }

    public ICollectionTargetBuilder<TItem, IModelMapper<TViewModel>> MapCollection<TItem>(Func<TViewModel, IEnumerable<TItem>> func)
    {
        var collection = new CollectionData(func);
        _collections.Add(collection);

        return new CollectionTargetBuilder<TItem, IModelMapper<TViewModel>>(collection, this);
    }

    public ModelMappingData Build() => new(_values, _collections);
}

