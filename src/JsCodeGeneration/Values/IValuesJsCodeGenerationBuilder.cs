using System.Text;

namespace Vitraux.JsCodeGeneration.Values;

internal interface IValuesJsCodeGenerationBuilder
{
    StringBuilder BuildJsCode(StringBuilder jsBuilder, string parentObjectName, IEnumerable<FullValueObjectName> values, int indentCount);
}
