namespace Vitraux.JsCodeGeneration;

internal interface IJsGenerator
{
    GeneratedJsCode GenerateJs(ModelMappingData modelMappingData, QueryElementStrategy queryElementStrategy, string parentObjectName, string parentElementObjectName, string elementNamePrefix);
}
