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
            .Aggregate(new StringBuilder(), (sb, value) =>
            {
                var targetJsCode = targetElementsValueJsBuilder.GenerateJs(parentObjectName, value, jsObjectNames);
                var propertyCheckerJsCode = propertyChecker.GenerateJs(parentObjectName, value.Name, targetJsCode);

                return sb
                    .AppendLine(propertyCheckerJsCode)
                    .AppendLine();
            })
            .ToString()
            .TrimEnd();
}
