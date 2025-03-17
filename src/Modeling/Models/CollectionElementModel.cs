using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Insertion;

namespace Vitraux.Modeling.Models;

internal record class CollectionElementModel(Delegate CollectionFunc)
{
    internal CollectionSelector CollectionSelector { get; set; } = default!;
    internal ModelMappingData ModelMappingData { get; set; } = default!;
}

internal record class CollectionSelector(ElementSelectorBase AppendToElementSelector)
{
    internal InsertionSelectorBase InsertionSelector { get; set; } = default!;
}