using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration;

internal interface IUniqueSelectorsFilter
{
    IEnumerable<ElementSelectorBase> FilterDistinct(IModelMappingData modelMappingData);
}
