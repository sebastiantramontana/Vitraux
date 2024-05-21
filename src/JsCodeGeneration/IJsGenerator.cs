using Vitraux.Modeling.Building.ModelMappers;

namespace Vitraux.JsCodeGeneration;

internal interface IJsGenerator<TViewModel>
{
    string GenerateJsCode(IModelMappingData modelMappingData, ConfigurationBehavior configurationBehavior);
}
