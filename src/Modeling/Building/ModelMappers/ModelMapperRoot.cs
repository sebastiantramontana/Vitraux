using Vitraux.Modeling.Building.ElementBuilders;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building.ModelMappers;

internal class ModelMapperRoot<TViewModel> : IModelMapperRoot<TViewModel>
{
    private readonly ICollection<ValueModel> _values = [];
    private readonly ICollection<CollectionElementModel> _collections = [];

    public IElementBuilderRoot<TViewModel> MapValue<TReturn>(Func<TViewModel, TReturn> func)
    {
        var valueModel = new ValueModel(func);
        _values.Add(valueModel);

        return new MapValueBuilder<TViewModel>(valueModel, this);
    }

    public IPopulationForCollectionBuilder<TReturn, IModelMapperRoot<TViewModel>> MapCollection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func)
        => new MapCollectionBuilder<TReturn, IModelMapperRoot<TViewModel>>(_collections, func, this);

    IEnumerable<ValueModel> IModelMappingData.Values => _values;
    IEnumerable<CollectionElementModel> IModelMappingData.CollectionElements => _collections;
}
