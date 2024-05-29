using Vitraux.Modeling.Building.ElementBuilders;
using Vitraux.Modeling.Building.Finallizables;

namespace Vitraux.Modeling.Building.ModelMappers;

public interface IModelMapperCollection<TViewModel, TModelMapperBack> : IModelMapper<TViewModel, IElementBuilderCollection<TViewModel, TModelMapperBack>, IModelMapperCollection<TViewModel, TModelMapperBack>, IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>, TViewModel>>
{
    TModelMapperBack EndCollection { get; }
}