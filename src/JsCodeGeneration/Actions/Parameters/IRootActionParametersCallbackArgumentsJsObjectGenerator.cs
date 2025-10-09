using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal interface IRootActionParametersCallbackArgumentsJsObjectGenerator
{
    StringBuilder GenerateJs(StringBuilder jsBuilder, ActionData action, IEnumerable<JsElementObjectName> jsParameterObjectNames, int indentCount);
}
