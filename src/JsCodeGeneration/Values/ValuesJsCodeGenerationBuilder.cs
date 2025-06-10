using System.Text;

namespace Vitraux.JsCodeGeneration.Values;

internal class ValuesJsCodeGenerationBuilder(
    IPropertyCheckerJsCodeGeneration propertyChecker,
    ITargetElementsValueJsGenerator targetElementsValueJsBuilder)
    : IValuesJsCodeGenerationBuilder
{
    public BuiltValueJs BuildJsCode(string parentObjectName, IEnumerable<FullValueObjectName> values)
    {
        var jsCode = values
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

        var valueViewModelSerializationsData = values
                .Select(value => new ValueViewModelSerializationData(value.Name, value.AssociatedData.DataFunc));
                

        return new BuiltValueJs(jsCode, valueViewModelSerializationsData);
    }
}
