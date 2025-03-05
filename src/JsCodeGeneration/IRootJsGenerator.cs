using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.Modeling.Building.ModelMappers;

namespace Vitraux.JsCodeGeneration;

internal interface IRootJsGenerator
{
    string GenerateJsCode(IModelMappingData modelMappingData, QueryElementStrategy queryElementStrategy);
}
