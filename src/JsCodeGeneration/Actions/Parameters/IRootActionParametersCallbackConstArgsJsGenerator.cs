using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal interface IRootActionParametersCallbackConstArgsJsGenerator
{
    StringBuilder GenerateJs(StringBuilder jsBuilder, string argsJsObjectName, ActionData action, IEnumerable<JsElementObjectName> jsParameterObjectNames, int currentIndentCount);
}
