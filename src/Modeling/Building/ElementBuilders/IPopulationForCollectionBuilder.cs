namespace Vitraux.Modeling.Building.ElementBuilders;

public interface IPopulationForCollectionBuilder<TViewModel, TModelMapperBack>
{
    IDocumentElementSelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>> ToTable { get; }
}