using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.Values;

internal class ValuesJsCodeGenerator(IValuesJsCodeGenerationBuilder builder) : IValuesJsCodeGenerator
{
    public string GenerateJsCode(IEnumerable<ValueObjectName> values, IEnumerable<ElementObjectName> elements)
        => builder.Build(values, elements);
}
