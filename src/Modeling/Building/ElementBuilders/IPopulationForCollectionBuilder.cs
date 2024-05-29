using Vitraux.Modeling.Building.ModelMappers;

namespace Vitraux.Modeling.Building.ElementBuilders;

public interface IPopulationForCollectionBuilder<TViewModel, TModelMapperBack>
{
    IDocumentElementSelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>, TViewModel> ToTable { get; }
    IDocumentElementSelectorBuilder<IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>, TViewModel> ByPopulatingElements { get; }
}
