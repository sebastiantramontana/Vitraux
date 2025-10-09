using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.JsCodeGeneration.Actions;

internal class RootActionsJsGenerator(IRootSingleActionJsGenerator rootSingleActionJsGenerator) : IRootActionsJsGenerator
{
    public string GenerateJs(string vmKey, IEnumerable<ActionData> actions, IEnumerable<JsElementObjectName> jsAllElementObjectNames, QueryElementStrategy queryElementStrategy)
        => actions
            .Aggregate(new StringBuilder(), (jsBuilder, action)
                => rootSingleActionJsGenerator
                    .GenerateJs(jsBuilder, vmKey, action, jsAllElementObjectNames, queryElementStrategy))
                    .AppendLine()
            .ToString()
            .TrimEnd();
}
