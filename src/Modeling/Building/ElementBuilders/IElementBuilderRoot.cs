using Vitraux.Modeling.Building.Finallizables;

namespace Vitraux.Modeling.Building.ElementBuilders;

public interface IElementBuilderRoot<TViewModel> : IToElementsBuilder<TViewModel, IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>, TViewModel>>
{
    IDocumentElementSelectorBuilder<IPopulationToChildrenElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>, TViewModel> ByPopulatingElements { get; }
}