using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.JsObjectNames;

internal interface IJsFullObjectNamesGenerator
{
    FullObjectNames Generate(ModelMappingData modelMappingData, IEnumerable<JsObjectName> allJsElementObjectNames);
}
