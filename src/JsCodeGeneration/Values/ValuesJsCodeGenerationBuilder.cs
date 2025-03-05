using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.Values;

internal class ValuesJsCodeGenerationBuilder(
    IPropertyCheckerJsCodeGeneration propertyChecker,
    ITargetElementsValueJsCodeGenerationBuilder targetElementsValueJsBuilder)
    : IValuesJsCodeGenerationBuilder
{
    public string BuildJsCode(string parentObjectName, IEnumerable<ValueObjectName> values, IEnumerable<ElementObjectName> elements)
        => values
            .Aggregate(new StringBuilder(), (sb, value) => sb.AppendLine(propertyChecker.GenerateJsCode(parentObjectName, value.Name, targetElementsValueJsBuilder.Build(parentObjectName, value, elements))))
            .ToString()
            .TrimEnd();
}
