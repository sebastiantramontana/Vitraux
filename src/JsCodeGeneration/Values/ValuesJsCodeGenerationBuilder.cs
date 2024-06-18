using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.Values;

internal class ValuesJsCodeGenerationBuilder(
    IValueCheckJsCodeGeneration valueCheck,
    ITargetElementsJsCodeGenerationBuilder targetElementJsBuilder)
    : IValuesJsCodeGenerationBuilder
{
    public string BuildJsCode(string parentObjectName, IEnumerable<ValueObjectName> values, IEnumerable<ElementObjectName> elements)
        => values
            .Aggregate(new StringBuilder(), (sb, value) => sb.AppendLine(valueCheck.GenerateJsCode(parentObjectName, value.Name, targetElementJsBuilder.Build(value, elements))))
            .ToString();
}
