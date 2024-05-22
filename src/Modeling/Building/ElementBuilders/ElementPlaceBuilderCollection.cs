using Vitraux.Modeling.Building.Finallizables;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building.ElementBuilders;

//TODO: Duplicated code in MapValueBuilder
internal class ElementPlaceBuilderCollection<TViewModel, TModelMapperBack>(ValueModel valueModel, IModelMapperCollection<TViewModel, TModelMapperBack> modelMapper)
    : IElementBuilderCollection<TViewModel, TModelMapperBack>,
    IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>,
    IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>
{
    private TargetElement _currentTargetElement = default!;

    IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>
        IToElementsBuilder<TViewModel, IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>>.ToElements
        => this;

    IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>
        IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>.ByQuery(string query)
    {
        return CreateTargetElement(new ElementQuerySelector(query));
    }

    IFinallizableCollection<TViewModel, TModelMapperBack>
        IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>.ToContent
        => CreateFinallizable(new ContentElementPlace());

    IFinallizableCollection<TViewModel, TModelMapperBack>
        IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>.ToAttribute(string attribute)
    {
        return CreateFinallizable(new AttributeElementPlace(attribute));
    }

    private ElementPlaceBuilderCollection<TViewModel, TModelMapperBack> CreateTargetElement(ElementSelector selector)
    {
        _currentTargetElement = new TargetElement(selector);
        valueModel.TargetElements = valueModel.TargetElements.Append(_currentTargetElement);
        return this;
    }

    private FinallizableCollection<TViewModel, TModelMapperBack> CreateFinallizable(ElementPlace elementPlace)
    {
        _currentTargetElement.Place = elementPlace;
        return new FinallizableCollection<TViewModel, TModelMapperBack>(modelMapper, this);
    }
}
