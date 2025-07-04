namespace Vitraux.JsCodeGeneration.JsObjectNames;

internal interface IJsFullObjectNamesGenerator
{
    FullObjectNames Generate(ModelMappingData modelMappingData);
    FullObjectNames Generate(string elementNamePrefix, ModelMappingData modelMappingData, int nestingLevel);
}
