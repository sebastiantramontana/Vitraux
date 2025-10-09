using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal class RootActionOnlyOnceAtStartParameterInitQueryJsGenerator(IStorageElementActionJsLineGenerator storageLineGenerator) : IRootActionOnlyOnceAtStartParameterInitQueryJsGenerator
{
    public StringBuilder GenerateJs(StringBuilder stringBuilder, IEnumerable<JsElementObjectName> jsObjectNames)
        => jsObjectNames.Aggregate(stringBuilder, (sb, jsElementObjectName) => sb.AppendLine(GenerateJsLine(jsElementObjectName)));

    private string GenerateJsLine(JsElementObjectName jsElementObjectName)
        => storageLineGenerator.Generate(jsElementObjectName);
}
