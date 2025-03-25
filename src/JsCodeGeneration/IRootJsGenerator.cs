using Vitraux.JsCodeGeneration.QueryElements;

namespace Vitraux.JsCodeGeneration;

internal interface IRootJsGenerator
{
    string GenerateJsCode(ModelMappingData modelMappingData, QueryElementStrategy queryElementStrategy);
}
