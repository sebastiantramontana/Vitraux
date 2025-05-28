namespace Vitraux.JsCodeGeneration;

internal interface IRootJsGenerator
{
    string GenerateJs(ModelMappingData modelMappingData, QueryElementStrategy queryElementStrategy);
}
