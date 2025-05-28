namespace Vitraux.JsCodeGeneration;

internal interface IJsGenerator
{
    string GenerateJs(ModelMappingData modelMappingData, QueryElementStrategy queryElementStrategy, string parentObjectName, string parentElementObjectName, string elementNamePrefix);
}
