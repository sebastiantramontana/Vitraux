using System.Text;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal class RootActionParametersCallbackConstArgsJsGenerator(
    IRootActionParametersCallbackArgumentsJsObjectGenerator argumentsObjectJsGenerator,
    ICodeFormatter codeFormatter) : IRootActionParametersCallbackConstArgsJsGenerator
{
    public StringBuilder GenerateJs(StringBuilder jsBuilder, string argsJsObjectName, ActionData action, IEnumerable<JsElementObjectName> jsParameterObjectNames, int currentIndentCount)
        => jsBuilder
                .AddLine(GenerateArgsDeclaration, argsJsObjectName, currentIndentCount)
                .TryAddLine(argumentsObjectJsGenerator.GenerateJs, action, jsParameterObjectNames, currentIndentCount + 1)
                .Add(GenerateCloseArgs, currentIndentCount);

    private StringBuilder GenerateArgsDeclaration(StringBuilder jsBuilder, string argsJsObjectName, int indentCount)
        => jsBuilder.Append(codeFormatter.IndentLine($"const {argsJsObjectName} = {{", indentCount));

    private StringBuilder GenerateCloseArgs(StringBuilder jsBuilder, int indentCount)
    {
        const string CloseArgs = "};";
        return jsBuilder.Append(codeFormatter.IndentLine(CloseArgs, indentCount));
    }
}
