using Vitraux.Modeling.Building.Finallizables;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building.ElementBuilders;

internal class MapValueBuilder<TViewModel>(ValueModel valueModel, IModelMapperRoot<TViewModel> modelMapperRoot)
    : IElementBuilderRoot<TViewModel>,
    IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>, TViewModel>,
    IDocumentElementSelectorBuilder<IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>, TViewModel>,
    IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>,
    IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>,
    IRootValuePopulationToChildrenBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>,
    IRootValuePopulationChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
{
    private TargetElement _currentTargetElement = default!;
    private PopulatingAppendToElementSelectorBase _currentByPopulatingElementsAppendTo = default!;

    public IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>, TViewModel> ToElements
        => this;

    public IDocumentElementSelectorBuilder<IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>, TViewModel> ByPopulatingElements
        => this;

    IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>
        IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>, TViewModel>.ById(string id)
        => CreateTargetElement(new ElementIdSelectorString(id));

    IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>
        IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>, TViewModel>.ByQuery(string query) 
        => CreateTargetElement(new ElementQuerySelectorString(query));

    IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>
        IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>, TViewModel>.ById(Func<TViewModel, string> idFunc) 
        => CreateTargetElement(new ElementIdSelectorDelegate(idFunc));

    IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>
        IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>, TViewModel>.ByQuery(Func<TViewModel, string> queryFunc) 
        => CreateTargetElement(new ElementQuerySelectorDelegate(queryFunc));

    IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
        IDocumentElementSelectorBuilder<IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>, TViewModel>.ById(string id) 
        => SetElementToAppendForPopulating(new PopulatingAppendToElementIdSelectorString(id));

    IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
        IElementQuerySelectorBuilder<IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>, TViewModel>.ByQuery(string query) 
        => SetElementToAppendForPopulating(new PopulatingAppendToElementQuerySelectorString(query));

    IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
        IDocumentElementSelectorBuilder<IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>, TViewModel>.ById(Func<TViewModel, string> idFunc)
        => SetElementToAppendForPopulating(new PopulatingAppendToElementIdSelectorDelegate(idFunc));

    IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
        IElementQuerySelectorBuilder<IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>, TViewModel>.ByQuery(Func<TViewModel, string> queryFunc)
        => SetElementToAppendForPopulating(new PopulatingAppendToElementQuerySelectorDelegate(queryFunc));

    IFinallizableRoot<TViewModel>
        IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>.ToContent
        => CreateFinallizable(new ContentElementPlace());

    IFinallizableRoot<TViewModel>
        IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>.ToAttribute(string attribute) 
        => CreateFinallizable(new AttributeElementPlace(attribute));

    IRootValuePopulationToChildrenBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
        IPopulationToNextElementSelector<IRootValuePopulationToChildrenBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>>.FromTemplate(string templateid) 
        => CreatePopulatingElementTarget(new ElementTemplateSelectorString(templateid));

    IRootValuePopulationToChildrenBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
        IPopulationToNextElementSelector<IRootValuePopulationToChildrenBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>>.FromFetch(Uri uri) 
        => CreatePopulatingElementTarget(new ElementFetchSelectorUri(uri));

    IRootValuePopulationChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>
        IRootValuePopulationToChildrenBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>.ToChildren
        => this;

    IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>
        IRootValuePopulationChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>.ByQuery(string query) 
        => SetTargetChildElementForPopulating(new ElementQuerySelectorString(query));

    private MapValueBuilder<TViewModel> CreateTargetElement(ElementSelectorBase selector)
    {
        _currentTargetElement = new TargetElement(selector);
        valueModel.TargetElements = valueModel.TargetElements.Append(_currentTargetElement);
        return this;
    }

    private MapValueBuilder<TViewModel> SetElementToAppendForPopulating(PopulatingAppendToElementSelectorBase populatingAppendToElementSelector)
    {
        _currentByPopulatingElementsAppendTo = populatingAppendToElementSelector;
        return this;
    }

    private MapValueBuilder<TViewModel> CreatePopulatingElementTarget(PopulatingElementSelectorBase populatingElementSelector)
    {
        populatingElementSelector.ElementToAppend = _currentByPopulatingElementsAppendTo;

        _currentTargetElement = new TargetElement(populatingElementSelector);
        valueModel.TargetElements = valueModel.TargetElements.Append(_currentTargetElement);

        return this;
    }

    private MapValueBuilder<TViewModel> SetTargetChildElementForPopulating(ElementQuerySelectorString targetChildElement)
    {
        var populatingSelector = _currentTargetElement.Selector as PopulatingElementSelectorBase;
        populatingSelector!.TargetChildElement = targetChildElement;
        return this;
    }

    private FinallizableRoot<TViewModel> CreateFinallizable(ElementPlace elementPlace)
    {
        _currentTargetElement.Place = elementPlace;
        return new FinallizableRoot<TViewModel>(modelMapperRoot, this, this);
    }
}
