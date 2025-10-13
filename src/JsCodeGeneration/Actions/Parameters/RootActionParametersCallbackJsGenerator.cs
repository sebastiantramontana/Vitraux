using System.Text;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal class RootActionParametersCallbackJsGenerator(IRootActionParametersCallbackBodyJsGenerator callbackBodyJsGenerator) : IRootActionParametersCallbackJsGenerator
{
    public StringBuilder GenerateJs(StringBuilder jsBuilder, string callbackFunctionName, ActionData action, QueryElementStrategy queryElementStrategy, IEnumerable<JsElementObjectName> jsParameterObjectNames)
    {
        const string CallbackEventParameterName = "event";
        const string CloseCallback = "};";

        return jsBuilder
                .AppendLine(GenerateCallbackDeclaration(callbackFunctionName, CallbackEventParameterName))
                .Add(callbackBodyJsGenerator.GenerateJs, action, queryElementStrategy, jsParameterObjectNames, CallbackEventParameterName, 1)
                .AppendLine(CloseCallback);
    }

    private static string GenerateCallbackDeclaration(string callbackFunctionName, string callbackEventParameterName)
        => $"const {callbackFunctionName} = ({callbackEventParameterName}) => {{";
}
