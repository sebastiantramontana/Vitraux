using Vitraux.Modeling.Building.Finallizables;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building.ElementBuilders;

//TODO: Duplicated code in MapValueBuilder
internal class ElementPlaceBuilderCollection<TViewModel, TModelMapperBack>(ValueModel valueModel, IModelMapperCollection<TViewModel, TModelMapperBack> modelMapper)
    : IElementBuilderCollection<TViewModel, TModelMapperBack>,
    IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>, TViewModel>,
    IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>
{
    private TargetElement _currentTargetElement = default!;

    IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>, TViewModel>
        IToElementsBuilder<TViewModel, IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>, TViewModel>>.ToElements
        => this;

    IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>
        IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>, TViewModel>.ByQuery(string query) 
        => CreateTargetElement(new ElementQuerySelectorString(query));

    IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>
        IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>, TViewModel>.ByQuery(Func<TViewModel, string> queryFunc)
        => CreateTargetElement(new ElementQuerySelectorDelegate(queryFunc));

    IFinallizableCollection<TViewModel, TModelMapperBack>
        IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>.ToContent
        => CreateFinallizable(new ContentElementPlace());

    IFinallizableCollection<TViewModel, TModelMapperBack>
        IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>.ToAttribute(string attribute) 
        => CreateFinallizable(new AttributeElementPlace(attribute));

    private ElementPlaceBuilderCollection<TViewModel, TModelMapperBack> CreateTargetElement(ElementSelectorBase selector)
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
