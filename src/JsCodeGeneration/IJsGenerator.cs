using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.Modeling.Building.ModelMappers;

namespace Vitraux.JsCodeGeneration;

internal interface IJsGenerator
{
    string GenerateJsCode(IModelMappingData modelMappingData, QueryElementStrategy queryElementStrategy, string parentObjectName, string parentElementObjectName, string elementNamePrefix);
}
