using Vitraux.Modeling.Building.ElementBuilders;

namespace Vitraux.Modeling.Building.ModelMappers;

public interface IModelMapper<TViewModel, TElementBuilder, TModelMapperBack, TNextElementSelectorBuilder> : IModelMappingData
    where TElementBuilder : IToElementsBuilder<TViewModel, TNextElementSelectorBuilder>
    where TNextElementSelectorBuilder : IElementSelectorBuilder
{
    TElementBuilder MapValue<TReturn>(Func<TViewModel, TReturn> func);
    IPopulationForCollectionBuilder<TReturn, TModelMapperBack> MapCollection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func);
}