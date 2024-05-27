using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Insertion;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building.ElementBuilders;

internal class MapCollectionBuilder<TViewModel, TModelMapperBack>(ICollection<CollectionElementModel> collectionElements, Delegate collectionElementsFunc, TModelMapperBack modelMapperBack)
    : IPopulationForCollectionBuilder<TViewModel, TModelMapperBack>,
    IDocumentElementSelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>>,
    IDocumentElementSelectorBuilder<IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>>,
    ITableRowsBuilder<TViewModel, TModelMapperBack>,
    IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>
{
    private CollectionElementModel _currentCollectionElement = default!;

    IDocumentElementSelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>>
        IPopulationForCollectionBuilder<TViewModel, TModelMapperBack>.ToTable
        => AddNewTableToCollectionElements();

    IDocumentElementSelectorBuilder<IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>>
        IPopulationForCollectionBuilder<TViewModel, TModelMapperBack>.ByPopulatingElements
        => AddNewElementToCollectionElements();

    IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>
        ITableRowsBuilder<TViewModel, TModelMapperBack>.ByPopulatingRows
        => this;

    ITableRowsBuilder<TViewModel, TModelMapperBack>
        IDocumentElementSelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>>.ById(string id)
        => SetElementSelectorToCurrentCollectionElement(new ElementIdSelector(id));

    ITableRowsBuilder<TViewModel, TModelMapperBack>
        IElementQuerySelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>>.ByQuery(string query)
        => SetElementSelectorToCurrentCollectionElement(new ElementQuerySelector(query));

    IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>
        IDocumentElementSelectorBuilder<IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>>.ById(string id)
        => SetElementSelectorToCurrentCollectionElement(new ElementIdSelector(id));

    IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>
        IElementQuerySelectorBuilder<IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>>.ByQuery(string query)
        => SetElementSelectorToCurrentCollectionElement(new ElementQuerySelector(query));

    IModelMapperCollection<TViewModel, TModelMapperBack>
        IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>.FromTemplate(string templateid)
        => SetInsertionSelector(new TemplateInsertionSelector(templateid));

    IModelMapperCollection<TViewModel, TModelMapperBack>
        IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>.FromFetch(Uri uri)
        => SetInsertionSelector(new FetchInsertionSelector(uri));

    private MapCollectionBuilder<TViewModel, TModelMapperBack> AddNewTableToCollectionElements()
    {
        AddNewCollectionElementToCollectionElements(new CollectionTableModel(collectionElementsFunc));
        return this;
    }

    private MapCollectionBuilder<TViewModel, TModelMapperBack> AddNewElementToCollectionElements()
    {
        AddNewCollectionElementToCollectionElements(new CollectionElementModel(collectionElementsFunc));
        return this;
    }

    private void AddNewCollectionElementToCollectionElements(CollectionElementModel collectionElementModel)
    {
        _currentCollectionElement = collectionElementModel;
        collectionElements.Add(_currentCollectionElement);
    }

    private MapCollectionBuilder<TViewModel, TModelMapperBack> SetElementSelectorToCurrentCollectionElement(ElementSelector selector)
    {
        _currentCollectionElement.ElementSelector = selector;
        return this;
    }

    private ModelMapperCollection<TViewModel, TModelMapperBack> SetInsertionSelector(InsertionSelector insertionSelector)
    {
        ModelMapperCollection<TViewModel, TModelMapperBack> modelMapper = new(modelMapperBack);

        _currentCollectionElement.ModelMappingData = modelMapper;
        _currentCollectionElement.InsertionSelector = insertionSelector;

        return modelMapper;
    }
}
