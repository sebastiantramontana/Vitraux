using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.JsCodeGeneration.Actions;

internal interface IRootActionsJsGenerator
{
    string GenerateJs(string vmKey, IEnumerable<ActionData> actions, IEnumerable<JsElementObjectName> jsAllElementObjectNames, QueryElementStrategy queryElementStrategy);
}