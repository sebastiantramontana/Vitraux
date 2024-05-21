using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.Values;

internal interface IValuesJsCodeGenerator
{
    string GenerateJsCode(IEnumerable<ValueObjectName> values, IEnumerable<ElementObjectName> elements);
}
