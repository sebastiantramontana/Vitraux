using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Insertion;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building.ElementBuilders;

internal class MapCollectionBuilder<TViewModel, TModelMapperBack>(ICollection<CollectionElementModel> collectionElements, Delegate collectionElementsFunc, TModelMapperBack modelMapperBack)
    : IPopulationForCollectionBuilder<TViewModel, TModelMapperBack>,
    IDocumentElementSelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>, TViewModel>,
    IDocumentElementSelectorBuilder<IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>, TViewModel>,
    ITableRowsBuilder<TViewModel, TModelMapperBack>,
    IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>
{
    private CollectionElementModel _currentCollectionElement = default!;

    IDocumentElementSelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>, TViewModel>
        IPopulationForCollectionBuilder<TViewModel, TModelMapperBack>.ToTable
        => AddNewTableToCollectionElements();

    IDocumentElementSelectorBuilder<IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>, TViewModel>
        IPopulationForCollectionBuilder<TViewModel, TModelMapperBack>.ByPopulatingElements
        => AddNewElementToCollectionElements();

    IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>
        ITableRowsBuilder<TViewModel, TModelMapperBack>.ByPopulatingRows
        => this;

    ITableRowsBuilder<TViewModel, TModelMapperBack>
        IDocumentElementSelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>, TViewModel>.ById(string id)
        => SetElementSelectorToCurrentCollectionElement(new ElementIdSelectorString(id));

    ITableRowsBuilder<TViewModel, TModelMapperBack>
        IElementQuerySelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>, TViewModel>.ByQuery(string query)
        => SetElementSelectorToCurrentCollectionElement(new ElementQuerySelectorString(query));

    ITableRowsBuilder<TViewModel, TModelMapperBack>
        IDocumentElementSelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>, TViewModel>.ById(Func<TViewModel, string> idFunc)
        => SetElementSelectorToCurrentCollectionElement(new ElementIdSelectorDelegate(idFunc));

    ITableRowsBuilder<TViewModel, TModelMapperBack>
        IElementQuerySelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>, TViewModel>.ByQuery(Func<TViewModel, string> queryFunc)
        => SetElementSelectorToCurrentCollectionElement(new ElementQuerySelectorDelegate(queryFunc));

    IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>
        IDocumentElementSelectorBuilder<IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>, TViewModel>.ById(string id)
        => SetElementSelectorToCurrentCollectionElement(new ElementIdSelectorString(id));

    IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>
        IElementQuerySelectorBuilder<IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>, TViewModel>.ByQuery(string query)
        => SetElementSelectorToCurrentCollectionElement(new ElementQuerySelectorString(query));

    IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>
        IDocumentElementSelectorBuilder<IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>, TViewModel>.ById(Func<TViewModel, string> idFunc)
        => SetElementSelectorToCurrentCollectionElement(new ElementIdSelectorDelegate(idFunc));

    IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>
        IElementQuerySelectorBuilder<IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>, TViewModel>.ByQuery(Func<TViewModel, string> queryFunc)
        => SetElementSelectorToCurrentCollectionElement(new ElementQuerySelectorDelegate(queryFunc));

    IModelMapperCollection<TViewModel, TModelMapperBack>
        IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>.FromTemplate(string templateid)
        => SetInsertionSelector(new TemplateInsertionSelectorString(templateid));

    IModelMapperCollection<TViewModel, TModelMapperBack>
        IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>.FromFetch(Uri uri)
        => SetInsertionSelector(new FetchInsertionSelectorUri(uri));

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

    private MapCollectionBuilder<TViewModel, TModelMapperBack> SetElementSelectorToCurrentCollectionElement(ElementSelectorBase selector)
    {
        _currentCollectionElement.CollectionSelector = new CollectionSelector(selector);
        return this;
    }

    private ModelMapperCollection<TViewModel, TModelMapperBack> SetInsertionSelector(InsertionSelectorBase insertionSelector)
    {
        ModelMapperCollection<TViewModel, TModelMapperBack> modelMapper = new(modelMapperBack);

        _currentCollectionElement.ModelMappingData = modelMapper;
        _currentCollectionElement.CollectionSelector.InsertionSelector = insertionSelector;

        return modelMapper;
    }
}
