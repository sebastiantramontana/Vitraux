using Vitraux.JsCodeGeneration.QueryElements;

namespace Vitraux.JsCodeGeneration;

internal interface IJsGenerator
{
    string GenerateJsCode(ModelMappingData modelMappingData, QueryElementStrategy queryElementStrategy, string parentObjectName, string parentElementObjectName, string elementNamePrefix);
}
