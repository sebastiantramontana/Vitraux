using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.Modeling.Building.ModelMappers;

namespace Vitraux.JsCodeGeneration;

internal interface IRootJsGenerator
{
    string GenerateJsCode(ModelMappingData modelMappingData, QueryElementStrategy queryElementStrategy);
}
