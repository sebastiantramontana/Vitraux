using Vitraux.Modeling.Building.Finallizables;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building.ElementBuilders;

internal class MapValueBuilder<TViewModel>(ValueModel valueModel, IModelMapperRoot<TViewModel> modelMapperRoot)
    : IElementBuilderRoot<TViewModel>,
    IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>,
    IDocumentElementSelectorBuilder<IAppendablePopulationElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>>,
    IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>,
    IAppendablePopulationElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>,
    IToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>,
    IPopulatingChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
{
    private TargetElement _currentTargetElement = default!;
    private PopulatingAppendToElementSelector _currentByPopulatingElementsAppendTo = default!;

    public IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
        ToElements => this;

    public IDocumentElementSelectorBuilder<IAppendablePopulationElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>>
        ByPopulatingElements => this;

    IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>
        IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>.ById(string id)
        => CreateTargetElement(new ElementIdSelector(id));

    IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>
        IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>.ByQuery(string query)
        => CreateTargetElement(new ElementQuerySelector(query));

    IAppendablePopulationElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
        IDocumentElementSelectorBuilder<IAppendablePopulationElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>>.ById(string id)
        => SetElementToAppendForPopulating(new PopulatingAppendToElementIdSelector(id));

    IAppendablePopulationElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
        IElementQuerySelectorBuilder<IAppendablePopulationElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>>.ByQuery(string query)
        => SetElementToAppendForPopulating(new PopulatingAppendToElementQuerySelector(query));

    IFinallizableRoot<TViewModel>
        IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>.ToContent
        => CreateFinallizable(new ContentElementPlace());

    IFinallizableRoot<TViewModel>
        IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>.ToAttribute(string attribute)
        => CreateFinallizable(new AttributeElementPlace(attribute));

    IToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
        IPopulationToNextElementSelector<IToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>>.FromTemplate(string templateid)
        => CreatePopulatingElementTarget(new ElementTemplateSelector(templateid));

    IToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
        IPopulationToNextElementSelector<IToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>>.FromFetch(Uri uri)
        => CreatePopulatingElementTarget(new ElementFetchSelector(uri));

    IPopulatingChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
        IToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>.ToChildren
        => this;

    IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>
        IPopulatingChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>.ByQuery(string query)
        => SetTargetChildElementForPopulating(new ElementQuerySelector(query));

    private MapValueBuilder<TViewModel> CreateTargetElement(ElementSelector selector)
    {
        _currentTargetElement = new TargetElement(selector);
        valueModel.TargetElements = valueModel.TargetElements.Append(_currentTargetElement);
        return this;
    }

    private MapValueBuilder<TViewModel> SetElementToAppendForPopulating(PopulatingAppendToElementSelector populatingAppendToElementSelector)
    {
        _currentByPopulatingElementsAppendTo = populatingAppendToElementSelector;
        return this;
    }

    private MapValueBuilder<TViewModel> CreatePopulatingElementTarget(PopulatingElementSelector populatingElementSelector)
    {
        populatingElementSelector.ElementToAppend = _currentByPopulatingElementsAppendTo;

        _currentTargetElement = new TargetElement(populatingElementSelector);
        valueModel.TargetElements = valueModel.TargetElements.Append(_currentTargetElement);

        return this;
    }

    private MapValueBuilder<TViewModel> SetTargetChildElementForPopulating(ElementQuerySelector targetChildElement)
    {
        var populatingSelector = _currentTargetElement.Selector as PopulatingElementSelector;
        populatingSelector!.TargetChildElement = targetChildElement;
        return this;
    }

    private FinallizableRoot<TViewModel> CreateFinallizable(ElementPlace elementPlace)
    {
        _currentTargetElement.Place = elementPlace;
        return new FinallizableRoot<TViewModel>(modelMapperRoot, this, this);
    }
}
