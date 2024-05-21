using Vitraux.Modeling.Building.ElementBuilders;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building.Finallizables;

internal class FinallizableCollection<TViewModel, TModelMapperBack>(
    IModelMapperCollection<TViewModel, TModelMapperBack> innerModelMapper,
    IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>> innerElementQuerySelectorBuilder)
    : IFinallizableCollection<TViewModel, TModelMapperBack>,
    IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>
{
    IEnumerable<ValueModel> IModelMappingData.Values => innerModelMapper.Values;
    IEnumerable<CollectionTableModel> IModelMappingData.CollectionTables => innerModelMapper.CollectionTables;

    public TModelMapperBack EndCollection => innerModelMapper.EndCollection;

    IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>
        IElementBuilder<TViewModel, IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>>.ToElements
        => this;

    IElementBuilderCollection<TViewModel, TModelMapperBack>
        IModelMapper<TViewModel, IElementBuilderCollection<TViewModel, TModelMapperBack>, IModelMapperCollection<TViewModel, TModelMapperBack>, IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>>.MapValue<TReturn>(Func<TViewModel, TReturn> func)
        => innerModelMapper.MapValue(func);

    ICollectionElementBuilder<TReturn, IModelMapperCollection<TViewModel, TModelMapperBack>>
        IModelMapper<TViewModel, IElementBuilderCollection<TViewModel, TModelMapperBack>, IModelMapperCollection<TViewModel, TModelMapperBack>, IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>>.MapCollection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func)
        => innerModelMapper.MapCollection(func);

    IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>
        IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>.ByQuery(string query)
        => innerElementQuerySelectorBuilder.ByQuery(query);
}