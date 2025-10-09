using System.Text;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal interface IRootActionParametersCallbackReturnArgsJsGenerator
{
    StringBuilder GenerateJs(StringBuilder jsBuilder, string argsJsObjectName, int indentCount);
}
