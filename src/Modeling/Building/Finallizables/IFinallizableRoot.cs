using Vitraux.Modeling.Building.ElementBuilders;
using Vitraux.Modeling.Building.ModelMappers;

namespace Vitraux.Modeling.Building.Finallizables;

public interface IFinallizableRoot<TViewModel> : IFinallizable<TViewModel>, IElementBuilderRoot<TViewModel>, IModelMapperRoot<TViewModel>
{
}