using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.Values;

internal class ValuesJsCodeGenerationBuilder(
    IPropertyCheckerJsCodeGeneration propertyChecker,
    ITargetElementsValueJsGenerator targetElementsValueJsBuilder)
    : IValuesJsCodeGenerationBuilder
{
    public string BuildJsCode(string parentObjectName, IEnumerable<ValueObjectName> values, IEnumerable<JsObjectName> jsObjectNames)
        => values
            .Aggregate(new StringBuilder(), (sb, value) => sb.AppendLine(propertyChecker.GenerateJsCode(parentObjectName, value.Name, targetElementsValueJsBuilder.GenerateJs(parentObjectName, value, jsObjectNames))))
            .ToString()
            .TrimEnd();
}
