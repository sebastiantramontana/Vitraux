using Vitraux.Modeling.Building.ModelMappers;

namespace Vitraux.Modeling.Building.ElementBuilders
{
    public interface ITableRowsBuilder<TViewModel, TModelMapperBack>
    {
        IPopulationToNextElementSelector<IModelMapperCollection<TViewModel, TModelMapperBack>> ByPopulatingRows { get; }
    }
}
