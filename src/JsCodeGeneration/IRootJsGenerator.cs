namespace Vitraux.JsCodeGeneration;

internal interface IRootJsGenerator
{
    GeneratedJsCode GenerateJs(ModelMappingData modelMappingData, QueryElementStrategy queryElementStrategy);
}
