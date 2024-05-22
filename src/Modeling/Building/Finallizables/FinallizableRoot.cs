﻿using Vitraux.Modeling.Building.ElementBuilders;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building.Finallizables;

internal class FinallizableRoot<TViewModel>(
    IModelMapperRoot<TViewModel> innerModelMapper,
    IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>> documentElementSelectorBuilder,
    IDocumentElementSelectorBuilder<IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>> documentAppendableElementSelectorBuilder)
    : IFinallizableRoot<TViewModel>,
    IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>,
    IDocumentElementSelectorBuilder<IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>>
{
    IEnumerable<ValueModel> IModelMappingData.Values => innerModelMapper.Values;
    IEnumerable<CollectionElementModel> IModelMappingData.CollectionElements => innerModelMapper.CollectionElements;

    IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
        IToElementsBuilder<TViewModel, IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>>.ToElements
        => this;

    IDocumentElementSelectorBuilder<IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>>
        IElementBuilderRoot<TViewModel>.ByPopulatingElements
        => this;

    IElementBuilderRoot<TViewModel>
        IModelMapper<TViewModel, IElementBuilderRoot<TViewModel>, IModelMapperRoot<TViewModel>, IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>>.MapValue<TReturn>(Func<TViewModel, TReturn> func)
        => innerModelMapper.MapValue(func);

    IPopulationForCollectionBuilder<TReturn, IModelMapperRoot<TViewModel>>
        IModelMapper<TViewModel, IElementBuilderRoot<TViewModel>, IModelMapperRoot<TViewModel>, IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>>.MapCollection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func)
        => innerModelMapper.MapCollection(func);

    IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>
        IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>.ById(string id)
        => documentElementSelectorBuilder.ById(id);

    IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>
        IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>.ByQuery(string query)
        => documentElementSelectorBuilder.ByQuery(query);

    IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
        IDocumentElementSelectorBuilder<IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>>.ById(string id)
        => documentAppendableElementSelectorBuilder.ById(id);

    IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
        IElementQuerySelectorBuilder<IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>>.ByQuery(string query)
        => documentAppendableElementSelectorBuilder.ByQuery(query);
}
