using Vitraux.Modeling.Data.Selectors;

namespace Vitraux.JsCodeGeneration.JsObjectNames;

internal interface IUniqueSelectorsFilter
{
    IEnumerable<SelectorBase> FilterDistinct(ModelMappingData modelMappingData);
}
