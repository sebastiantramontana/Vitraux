using System;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Insertion;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building.ElementBuilders;

internal class MapCollectionBuilder<TViewModel, TModelMapperBack>(ICollection<CollectionElementModel> collectionElements, Delegate collectionElementsFunc, TModelMapperBack modelMapperBack)
    : IPopulationForCollectionBuilder<TViewModel, TModelMapperBack>,
    IDocumentElementSelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>>,
    ITableRowsBuilder<TViewModel, TModelMapperBack>,
    IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>
{
    private CollectionElementModel _currentCollectionElement = default!;

    IDocumentElementSelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>>
        IPopulationForCollectionBuilder<TViewModel, TModelMapperBack>.ToTable
        => AddNewTableToCollectionElements();

    public IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>> ByPopulatingRows
        => this;

    ITableRowsBuilder<TViewModel, TModelMapperBack>
        IDocumentElementSelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>>.ById(string id)
        => SetElementSelectorToCurrentCollectionElement(new ElementIdSelector(id));

    ITableRowsBuilder<TViewModel, TModelMapperBack>
        IElementQuerySelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>>.ByQuery(string query)
        => SetElementSelectorToCurrentCollectionElement(new ElementQuerySelector(query));

    IModelMapperCollection<TViewModel, TModelMapperBack>
        IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>.FromTemplate(string templateid)
        => SetInsertionSelector(new TemplateInsertionSelector(templateid));

    IModelMapperCollection<TViewModel, TModelMapperBack>
        IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>.FromFetch(Uri uri)
        => SetInsertionSelector(new FetchInsertionSelector(uri));

    private MapCollectionBuilder<TViewModel, TModelMapperBack> AddNewTableToCollectionElements()
    {
        _currentCollectionElement = new CollectionTableModel(collectionElementsFunc);
        collectionElements.Add(_currentCollectionElement);
        return this;
    }

    private MapCollectionBuilder<TViewModel, TModelMapperBack> SetElementSelectorToCurrentCollectionElement(ElementSelector selector)
    {
        _currentCollectionElement.ElementSelector = selector;
        return this;
    }

    private ModelMapperCollection<TViewModel, TModelMapperBack> SetInsertionSelector(InsertionSelector insertionSelector)
    {
        var modelMapper = new ModelMapperCollection<TViewModel, TModelMapperBack>(modelMapperBack);

        _currentCollectionElement.ModelMappingData = modelMapper;
        _currentCollectionElement.InsertionSelector = insertionSelector;

        return modelMapper;
    }
}
