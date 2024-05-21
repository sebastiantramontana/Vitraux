using Vitraux.Modeling.Building.Finallizables;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building.ElementBuilders;

//TODO: Duplicated code in MapValueBuilder
internal class ElementPlaceBuilderCollection<TViewModel, TModelMapperBack>(ValueModel valueModel, IModelMapperCollection<TViewModel, TModelMapperBack> modelMapper)
    : IElementBuilderCollection<TViewModel, TModelMapperBack>,
    IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>,
    IDocumentElementSelectorBuilder<IAppendablePopulationElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>>,
    IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>,
    IAppendablePopulationElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>,
    IToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>,
    IPopulatingChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>
{
    private TargetElement _currentTargetElement = default!;
    private PopulatingAppendToElementSelector _currentByPopulatingElementsAppendTo = default!;

    IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>
        IElementBuilder<TViewModel, IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>>.ToElements
        => this;

    IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>
        IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>.ById(string id)
        => CreateTargetElement(new ElementIdSelector(id));

    IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>
        IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>.ByQuery(string query)
        => CreateTargetElement(new ElementQuerySelector(query));

    IAppendablePopulationElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>
        IDocumentElementSelectorBuilder<IAppendablePopulationElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>>.ById(string id)
        => SetElementToAppendForPopulating(new PopulatingAppendToElementIdSelector(id));

    IAppendablePopulationElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>
        IElementQuerySelectorBuilder<IAppendablePopulationElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>>.ByQuery(string query)
        => SetElementToAppendForPopulating(new PopulatingAppendToElementQuerySelector(query));

    IFinallizableCollection<TViewModel, TModelMapperBack>
        IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>.ToContent
        => CreateFinallizable(new ContentElementPlace());

    IFinallizableCollection<TViewModel, TModelMapperBack>
        IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>.ToAttribute(string attribute)
        => CreateFinallizable(new AttributeElementPlace(attribute));

    IToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>
        IPopulationToNextElementSelector<IToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>>.FromTemplate(string templateid)
        => CreatePopulatingElementTarget(new ElementTemplateSelector(templateid));

    IToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>
        IPopulationToNextElementSelector<IToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>>.FromFetch(Uri uri)
        => CreatePopulatingElementTarget(new ElementFetchSelector(uri));

    IPopulatingChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>
        IToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>.ToChildren
        => this;

    IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>
        IPopulatingChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>.ByQuery(string query)
        => SetTargetChildElementForPopulating(new ElementQuerySelector(query));

    private ElementPlaceBuilderCollection<TViewModel, TModelMapperBack> CreateTargetElement(ElementSelector selector)
    {
        _currentTargetElement = new TargetElement(selector);
        valueModel.TargetElements = valueModel.TargetElements.Append(_currentTargetElement);
        return this;
    }

    private ElementPlaceBuilderCollection<TViewModel, TModelMapperBack> SetElementToAppendForPopulating(PopulatingAppendToElementSelector populatingAppendToElementSelector)
    {
        _currentByPopulatingElementsAppendTo = populatingAppendToElementSelector;
        return this;
    }

    private ElementPlaceBuilderCollection<TViewModel, TModelMapperBack> CreatePopulatingElementTarget(PopulatingElementSelector populatingElementSelector)
    {
        populatingElementSelector.ElementToAppend = _currentByPopulatingElementsAppendTo;

        _currentTargetElement = new TargetElement(populatingElementSelector);
        valueModel.TargetElements = valueModel.TargetElements.Append(_currentTargetElement);

        return this;
    }

    private ElementPlaceBuilderCollection<TViewModel, TModelMapperBack> SetTargetChildElementForPopulating(ElementQuerySelector targetChildElement)
    {
        var populatingSelector = _currentTargetElement.Selector as PopulatingElementSelector;
        populatingSelector!.TargetChildElement = targetChildElement;
        return this;
    }

    private FinallizableCollection<TViewModel, TModelMapperBack> CreateFinallizable(ElementPlace elementPlace)
    {
        _currentTargetElement.Place = elementPlace;
        return new FinallizableCollection<TViewModel, TModelMapperBack>(modelMapper, this);
    }
}
