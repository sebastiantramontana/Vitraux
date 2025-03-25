namespace Vitraux.JsCodeGeneration;

internal interface IUniqueSelectorsFilter
{
    UniqueFilteredSelectors FilterDistinct(ModelMappingData modelMappingData);
}
