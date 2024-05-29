using Vitraux.Modeling.Building.ElementBuilders;
using Vitraux.Modeling.Building.Finallizables;

namespace Vitraux.Modeling.Building.ModelMappers;

public interface IModelMapperRoot<TViewModel> : IModelMapper<TViewModel, IElementBuilderRoot<TViewModel>, IModelMapperRoot<TViewModel>, IDocumentElementSelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableRoot<TViewModel>>, TViewModel>>
{
}