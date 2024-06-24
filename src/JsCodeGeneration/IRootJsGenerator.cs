using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.Modeling.Building.ModelMappers;

namespace Vitraux.JsCodeGeneration;

internal interface IRootJsGenerator<TViewModel>
{
    string GenerateJsCode(IModelMappingData modelMappingData, QueryElementStrategy queryElementStrategy);
}
