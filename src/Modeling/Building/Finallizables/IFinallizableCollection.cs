using Vitraux.Modeling.Building.ElementBuilders;
using Vitraux.Modeling.Building.ModelMappers;

namespace Vitraux.Modeling.Building.Finallizables;

public interface IFinallizableCollection<TViewModel, TModelMapperBack> : IFinallizable<TViewModel>, IElementBuilderCollection<TViewModel, TModelMapperBack>, IModelMapperCollection<TViewModel, TModelMapperBack>
{
}