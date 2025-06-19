using System.Text;

namespace Vitraux.JsCodeGeneration.Values;

internal class ValuesJsCodeGenerationBuilder(
    IPropertyCheckerJsCodeGeneration propertyChecker,
    ITargetElementsValueJsGenerator targetElementsValueJsBuilder)
    : IValuesJsCodeGenerationBuilder
{
    public string BuildJsCode(string parentObjectName, IEnumerable<FullValueObjectName> values)
        => values
            .Aggregate(new StringBuilder(), (sb, value) =>
            {
                var targetJsCode = targetElementsValueJsBuilder.GenerateJs(parentObjectName, value);
                var propertyCheckerJsCode = propertyChecker.GenerateJs(parentObjectName, value.Name, targetJsCode);

                return sb
                    .AppendLine(propertyCheckerJsCode)
                    .AppendLine();
            })
            .ToString()
            .TrimEnd();
}
