using System.Text;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal class RootActionOnlyOnceAtStartParameterInitQueryJsGenerator(IStorageElementActionJsLineGenerator storageLineGenerator) : IRootActionOnlyOnceAtStartParameterInitQueryJsGenerator
{
    public StringBuilder GenerateJs(StringBuilder jsBuilder, IEnumerable<JsElementObjectName> jsObjectNames)
        => jsObjectNames.Any()
            ? jsObjectNames
                .Aggregate(jsBuilder, (jsb, jsElementObjectName) => jsb.AppendLine(GenerateJsLine(jsElementObjectName)))
                .TrimEnd()
            : jsBuilder;

    private string GenerateJsLine(JsElementObjectName jsElementObjectName)
        => storageLineGenerator.Generate(jsElementObjectName);
}
