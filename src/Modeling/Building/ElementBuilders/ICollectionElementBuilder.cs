namespace Vitraux.Modeling.Building.ElementBuilders;

public interface ICollectionElementBuilder<TViewModel, TModelMapperBack>
{
    IDocumentElementSelectorBuilder<ITableRowsBuilder<TViewModel, TModelMapperBack>> ToTable { get; }
}