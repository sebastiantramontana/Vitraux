using System.Text;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.JsCodeGeneration.Actions;

internal class RootActionsJsGenerator(IRootSingleActionJsGenerator rootSingleActionJsGenerator) : IRootActionsJsGenerator
{
    private const string UseStrict = "'use strict';";

    public string GenerateJs(string vmKey, IEnumerable<ActionData> actions, IEnumerable<JsElementObjectName> jsAllElementObjectNames, QueryElementStrategy queryElementStrategy)
    {
        var jsBuilder = CreateJsBuilder()
                        .AddLine(AddUseStrict);

        return actions
                .Aggregate(jsBuilder, (jsb, action)
                    => jsb.AddTwoLines(rootSingleActionJsGenerator.GenerateJs, vmKey, action, jsAllElementObjectNames, queryElementStrategy))
                .ToString()
                .TrimEnd();
    }

    private static StringBuilder CreateJsBuilder()
        => new();

    private static StringBuilder AddUseStrict(StringBuilder jsBuilder)
        => jsBuilder.AppendLine(UseStrict);
}
