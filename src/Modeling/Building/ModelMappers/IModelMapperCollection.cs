using Vitraux.Modeling.Building.ElementBuilders.Values;

namespace Vitraux.Modeling.Building.ModelMappers;

public interface IModelMapperCollection<TViewModel, TModelMapperBack> : IModelMapper<TViewModel, IElementBuilderCollection<TViewModel, TModelMapperBack>, IModelMapperCollection<TViewModel, TModelMapperBack>, IElementQuerySelectorBuilder<IValueElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>, TViewModel>>
{
    TModelMapperBack EndCollection { get; }
}