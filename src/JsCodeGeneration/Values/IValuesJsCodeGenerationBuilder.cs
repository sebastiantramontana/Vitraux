using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.Values;

internal interface IValuesJsCodeGenerationBuilder
{
    string BuildJsCode(string parentObjectName, IEnumerable<ValueObjectName> values, IEnumerable<JsObjectName> jsObjectNames);
}
