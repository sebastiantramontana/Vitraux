using Vitraux.Modeling.Building.ElementBuilders;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building.Finallizables;

internal class FinallizableRoot<TViewModel>(
    IModelMapperRoot<TViewModel> innerModelMapper,
    IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>, TViewModel> documentElementSelectorBuilder,
    IDocumentElementSelectorBuilder<IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>, TViewModel> documentAppendableElementSelectorBuilder)
    : IFinallizableRoot<TViewModel>,
    IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>, TViewModel>,
    IDocumentElementSelectorBuilder<IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>, TViewModel>
{
    IEnumerable<ValueModel> IModelMappingData.Values => innerModelMapper.Values;
    IEnumerable<CollectionElementModel> IModelMappingData.CollectionElements => innerModelMapper.CollectionElements;

    IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>, TViewModel>
        IToElementsBuilder<TViewModel, IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>, TViewModel>>.ToElements
        => this;

    IDocumentElementSelectorBuilder<IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>, TViewModel>
        IElementBuilderRoot<TViewModel>.ByPopulatingElements
        => this;

    IElementBuilderRoot<TViewModel>
        IModelMapper<TViewModel, IElementBuilderRoot<TViewModel>, IModelMapperRoot<TViewModel>, IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>, TViewModel>>.MapValue<TReturn>(Func<TViewModel, TReturn> func)
        => innerModelMapper.MapValue(func);

    IPopulationForCollectionBuilder<TReturn, IModelMapperRoot<TViewModel>>
        IModelMapper<TViewModel, IElementBuilderRoot<TViewModel>, IModelMapperRoot<TViewModel>, IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>, TViewModel>>.MapCollection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func)
        => innerModelMapper.MapCollection(func);

    IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>
        IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>, TViewModel>.ById(string id)
        => documentElementSelectorBuilder.ById(id);

    IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>
        IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>, TViewModel>.ByQuery(string query)
        => documentElementSelectorBuilder.ByQuery(query);

    IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>
        IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>, TViewModel>.ById(Func<TViewModel, string> idFunc)
        => documentElementSelectorBuilder.ById(idFunc);

    IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>
        IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>, TViewModel>.ByQuery(Func<TViewModel, string> queryFunc)
        => documentElementSelectorBuilder.ByQuery(queryFunc);

    IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
        IDocumentElementSelectorBuilder<IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>, TViewModel>.ById(string id)
        => documentAppendableElementSelectorBuilder.ById(id);

    IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
        IElementQuerySelectorBuilder<IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>, TViewModel>.ByQuery(string query)
        => documentAppendableElementSelectorBuilder.ByQuery(query);

    IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>> 
        IDocumentElementSelectorBuilder<IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>, TViewModel>.ById(Func<TViewModel, string> idFunc)
        => documentAppendableElementSelectorBuilder.ById(idFunc);

    IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>> 
        IElementQuerySelectorBuilder<IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>, TViewModel>.ByQuery(Func<TViewModel, string> queryFunc)
        => documentAppendableElementSelectorBuilder.ByQuery(queryFunc);
}
