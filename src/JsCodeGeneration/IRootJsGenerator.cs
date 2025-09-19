using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.JsCodeGeneration;

internal interface IRootJsGenerator
{
    GeneratedJsCode GenerateJs(FullObjectNames fullObjectNames, QueryElementStrategy queryElementStrategy);
}

internal interface IRootActionsJsGenerator
{
    string GenerateJs(IEnumerable<ActionData> actions, QueryElementStrategy queryElementStrategy);
}


