using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.TableRows;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building.ElementBuilders;

internal class MapCollectionBuilder<TViewModel, TModelMapperBack>(CollectionTableModel collectionTableModel, TModelMapperBack modelMapperBack)
    : ICollectionElementBuilder<TViewModel, TModelMapperBack>,
    IDocumentElementSelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>>,
    ITableRowsBuilder<TViewModel, TModelMapperBack>,
    IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>
{
    IDocumentElementSelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>>
        ICollectionElementBuilder<TViewModel, TModelMapperBack>.ToTable
        => this;

    public IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>> ByPopulatingRows
        => this;

    public ITableRowsBuilder<TViewModel, TModelMapperBack> ById(string id)
        => SetTableSelector(new ElementIdSelector(id));

    public ITableRowsBuilder<TViewModel, TModelMapperBack> ByQuery(string query)
        => SetTableSelector(new ElementQuerySelector(query));

    public IModelMapperCollection<TViewModel, TModelMapperBack> FromTemplate(string templateid)
        => SetRowsSelector(new TemplateRowSelector(templateid));

    public IModelMapperCollection<TViewModel, TModelMapperBack> FromFetch(Uri uri)
        => SetRowsSelector(new FetchRowSelector(uri));

    private MapCollectionBuilder<TViewModel, TModelMapperBack> SetTableSelector(ElementSelector selector)
    {
        collectionTableModel.TableSelector = selector;
        return this;
    }

    private ModelMapperCollection<TViewModel, TModelMapperBack> SetRowsSelector(RowSelector rowSelection)
    {
        var modelMapper = new ModelMapperCollection<TViewModel, TModelMapperBack>(modelMapperBack);

        collectionTableModel.ModelMappingData = modelMapper;
        collectionTableModel.RowSelector = rowSelection;

        return modelMapper;
    }
}
