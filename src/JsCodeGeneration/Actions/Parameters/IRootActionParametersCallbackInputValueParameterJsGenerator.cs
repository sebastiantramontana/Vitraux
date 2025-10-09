using System.Text;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal interface IRootActionParametersCallbackInputValueParameterJsGenerator
{
    StringBuilder GenerateJs(StringBuilder jsBuilder, bool passInputValueParameter, string argsJsObjectName, string callbackEventParameterName, int indentCount);
}
