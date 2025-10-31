using System.Text;
using Vitraux.JsCodeGeneration.Formating;

namespace Vitraux.JsCodeGeneration.Values;

internal class ValuesJsCodeGenerationBuilder(
    IPropertyCheckerJsCodeGeneration propertyChecker,
    ITargetElementsValueJsGenerator targetElementsValueJsBuilder)
    : IValuesJsCodeGenerationBuilder
{
    public StringBuilder BuildJsCode(StringBuilder jsBuilder, string parentObjectName, IEnumerable<FullValueObjectName> values, int indentCount)
        => values.Any()
            ? values
                .Aggregate(jsBuilder, (sb, value)
                    => sb
                    .AddLine(propertyChecker.GenerateBeginCheckJs, parentObjectName, value.Name, indentCount)
                    .TryAddLine(targetElementsValueJsBuilder.GenerateJs, parentObjectName, value, indentCount + 1)
                    .AddTwoLines(propertyChecker.GenerateEndCheckJs, indentCount))
                .TrimEnd()
            : jsBuilder;
}
