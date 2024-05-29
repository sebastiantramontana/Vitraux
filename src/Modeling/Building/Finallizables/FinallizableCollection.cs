using Vitraux.Modeling.Building.ElementBuilders;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building.Finallizables;

internal class FinallizableCollection<TViewModel, TModelMapperBack>(
    IModelMapperCollection<TViewModel, TModelMapperBack> innerModelMapper,
    IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>, TViewModel> innerElementQuerySelectorBuilder)
    : IFinallizableCollection<TViewModel, TModelMapperBack>,
    IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>, TViewModel>
{
    IEnumerable<ValueModel> IModelMappingData.Values => innerModelMapper.Values;
    IEnumerable<CollectionElementModel> IModelMappingData.CollectionElements => innerModelMapper.CollectionElements;

    public TModelMapperBack EndCollection => innerModelMapper.EndCollection;

    IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>, TViewModel>
        IToElementsBuilder<TViewModel, IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>, TViewModel>>.ToElements
        => this;

    IElementBuilderCollection<TViewModel, TModelMapperBack>
        IModelMapper<TViewModel, IElementBuilderCollection<TViewModel, TModelMapperBack>, IModelMapperCollection<TViewModel, TModelMapperBack>, IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>, TViewModel>>.MapValue<TReturn>(Func<TViewModel, TReturn> func)
        => innerModelMapper.MapValue(func);

    IPopulationForCollectionBuilder<TReturn, IModelMapperCollection<TViewModel, TModelMapperBack>>
        IModelMapper<TViewModel, IElementBuilderCollection<TViewModel, TModelMapperBack>, IModelMapperCollection<TViewModel, TModelMapperBack>, IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>, TViewModel>>.MapCollection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func)
        => innerModelMapper.MapCollection(func);

    IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>
        IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>, TViewModel>.ByQuery(string query)
        => innerElementQuerySelectorBuilder.ByQuery(query);

    IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>
        IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>, TViewModel>.ByQuery(Func<TViewModel, string> queryFunc)
        => innerElementQuerySelectorBuilder.ByQuery(queryFunc);
}