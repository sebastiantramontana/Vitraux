using Vitraux.Modeling.Building.ElementBuilders;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building.ModelMappers;

internal class ModelMapperCollection<TViewModel, TModelMapperBack>(TModelMapperBack modelMapperBack)
    : IModelMapperCollection<TViewModel, TModelMapperBack>
{
    private readonly ICollection<ValueModel> _values = [];
    private readonly ICollection<CollectionTableModel> _collections = [];

    IEnumerable<ValueModel> IModelMappingData.Values => _values;
    IEnumerable<CollectionTableModel> IModelMappingData.CollectionTables => _collections;

    public TModelMapperBack EndCollection => modelMapperBack;

    public IElementBuilderCollection<TViewModel, TModelMapperBack> MapValue<TReturn>(Func<TViewModel, TReturn> func)
    {
        var valueModel = new ValueModel(func);
        _values.Add(valueModel);

        return new ElementPlaceBuilderCollection<TViewModel, TModelMapperBack>(valueModel, this);
    }

    public ICollectionElementBuilder<TReturn, IModelMapperCollection<TViewModel, TModelMapperBack>> MapCollection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func)
    {
        var collectionTableModel = new CollectionTableModel(func);
        _collections.Add(collectionTableModel);

        return new MapCollectionBuilder<TReturn, IModelMapperCollection<TViewModel, TModelMapperBack>>(collectionTableModel, this);
    }
}
