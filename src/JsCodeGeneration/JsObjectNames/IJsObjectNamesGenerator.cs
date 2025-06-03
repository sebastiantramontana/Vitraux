namespace Vitraux.JsCodeGeneration.JsObjectNames;

internal interface IJsObjectNamesGenerator
{
    JsObjectNamesGrouping Generate(ModelMappingData modelMappingData, string elementNamePrefix);
}
