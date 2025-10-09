using System.Text;
using Vitraux.JsCodeGeneration.Formating;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal class RootActionParametersCallbackInputValueParameterJsGenerator(ICodeFormatter codeFormatter) : IRootActionParametersCallbackInputValueParameterJsGenerator
{
    public StringBuilder GenerateJs(StringBuilder jsBuilder, bool passInputValueParameter, string argsJsObjectName, string callbackEventParameterName, int indentCount) 
        => passInputValueParameter
            ? jsBuilder.AppendLine(codeFormatter.Indent(GenerateSettingInputValueJs(argsJsObjectName, callbackEventParameterName), indentCount))
            : jsBuilder;

    private static string GenerateSettingInputValueJs(string argsJsObjectName, string callbackEventParameterName)
        => $"{argsJsObjectName}.inputValue = {callbackEventParameterName}.target.value;";
}