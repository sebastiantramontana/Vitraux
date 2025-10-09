using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal interface IRootActionParametersCallbackBodyJsGenerator
{
    StringBuilder GenerateJs(StringBuilder jsBuilder, ActionData action, QueryElementStrategy queryElementStrategy, IEnumerable<JsElementObjectName> jsParameterObjectNames, string callbackEventParameterName, int indentCount);
}
