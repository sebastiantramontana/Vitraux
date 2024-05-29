using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Insertion;

namespace Vitraux.Modeling.Models;

internal record class CollectionElementModel(Delegate CollectionFunc)
{
    internal ElementSelectorBase ElementSelector { get; set; } = default!;
    internal InsertionSelectorBase InsertionSelector { get; set; } = default!;
    internal IModelMappingData ModelMappingData { get; set; } = default!;
}
