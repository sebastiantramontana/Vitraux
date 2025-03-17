using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.Modeling.Building.ModelMappers;

namespace Vitraux.JsCodeGeneration;

internal class RootJsGenerator(IJsGenerator jsGenerator) : IRootJsGenerator
{
    public string GenerateJsCode(ModelMappingData modelMappingData, QueryElementStrategy queryElementStrategy)
        => jsGenerator.GenerateJsCode(modelMappingData, queryElementStrategy, "vm", "document", string.Empty);
}
