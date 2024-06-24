using Vitraux.Modeling.Building.ModelMappers;

namespace Vitraux.JsCodeGeneration;

internal interface IJsGenerator<TViewModel>
{
    string GenerateJsCode(IModelMappingData modelMappingData, string parentObjectForValues, string elementNamePrefix);
}
