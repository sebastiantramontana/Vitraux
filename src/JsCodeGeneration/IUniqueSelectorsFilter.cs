using Vitraux.Modeling.Data.Selectors;

namespace Vitraux.JsCodeGeneration;

internal interface IUniqueSelectorsFilter
{
    IEnumerable<SelectorBase> FilterDistinct(ModelMappingData modelMappingData);
}
