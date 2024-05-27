using Vitraux.Modeling.Building.ModelMappers;

namespace Vitraux.Modeling.Building.ElementBuilders;

public interface IPopulationForCollectionBuilder<TViewModel, TModelMapperBack>
{
    IDocumentElementSelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>> ToTable { get; }
    IDocumentElementSelectorBuilder<IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>>> ByPopulatingElements { get; }
}
