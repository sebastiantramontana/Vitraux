using Vitraux.Modeling.Building.ModelMappers;

namespace Vitraux.JsCodeGeneration;

internal interface IUniqueSelectorsFilter
{
    UniqueFilteredSelectors FilterDistinct(ModelMappingData modelMappingData);
}
