using Vitraux.Modeling.Building.Finallizables;

namespace Vitraux.Modeling.Building.ElementBuilders;

public interface IElementBuilderRoot<TViewModel> : IElementBuilder<TViewModel, IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>>
{
    IDocumentElementSelectorBuilder<IAppendablePopulationElementSelector<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>>> ByPopulatingElements { get; }
}