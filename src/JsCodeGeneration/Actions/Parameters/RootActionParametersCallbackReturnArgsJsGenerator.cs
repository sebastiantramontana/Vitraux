using System.Text;
using Vitraux.JsCodeGeneration.Formating;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal class RootActionParametersCallbackReturnArgsJsGenerator(ICodeFormatter codeFormatter) : IRootActionParametersCallbackReturnArgsJsGenerator
{
    public StringBuilder GenerateJs(StringBuilder jsBuilder, string argsJsObjectName, int indentCount) 
        => jsBuilder.Append(codeFormatter.IndentLine(GenerateReturnArgsJs(argsJsObjectName), indentCount));

    private static string GenerateReturnArgsJs(string argsJsObjectName)
        => $"return {argsJsObjectName};";
}
