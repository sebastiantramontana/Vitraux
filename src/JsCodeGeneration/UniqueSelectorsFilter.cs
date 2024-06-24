using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration;

internal class UniqueSelectorsFilter : IUniqueSelectorsFilter
{
    public IEnumerable<ElementSelectorBase> FilterDistinct(IModelMappingData modelMappingData)
    {
        return modelMappingData
                .Values
                .SelectMany(v => v.TargetElements.Select(te => te.Selector))
                .Distinct()
                .Concat(modelMappingData.CollectionElements.Select(c => c.ElementSelector));
    }
}
