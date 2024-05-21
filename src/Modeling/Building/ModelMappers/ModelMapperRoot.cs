using Vitraux.Modeling.Building.ElementBuilders;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building.ModelMappers;

internal class ModelMapperRoot<TViewModel> : IModelMapperRoot<TViewModel>
{
    private readonly ICollection<ValueModel> _values = [];
    private readonly ICollection<CollectionTableModel> _collections = [];

    public IElementBuilderRoot<TViewModel> MapValue<TReturn>(Func<TViewModel, TReturn> func)
    {
        var valueModel = new ValueModel(func);
        _values.Add(valueModel);

        return new MapValueBuilder<TViewModel>(valueModel, this);
    }

    public ICollectionElementBuilder<TReturn, IModelMapperRoot<TViewModel>> MapCollection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func)
    {
        var collectionTableModel = new CollectionTableModel(func);
        _collections.Add(collectionTableModel);

        return new MapCollectionBuilder<TReturn, IModelMapperRoot<TViewModel>>(collectionTableModel, this);
    }

    IEnumerable<ValueModel> IModelMappingData.Values => _values;
    IEnumerable<CollectionTableModel> IModelMappingData.CollectionTables => _collections;
}
