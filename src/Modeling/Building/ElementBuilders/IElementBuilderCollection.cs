using Vitraux.Modeling.Building.Finallizables;

namespace Vitraux.Modeling.Building.ElementBuilders;

public interface IElementBuilderCollection<TViewModel, TModelMapperBack> : IElementBuilder<TViewModel, IElementQuerySelectorBuilder<IElementPlaceBuilder<TViewModel, IFinallizableCollection<TViewModel, TModelMapperBack>>>>
{
}
