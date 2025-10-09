using System.Text;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal class RootActionParametersCallbackQueryElementsJsGenerator(
    IQueryElementsJsCodeGeneratorContext queryElementsJsCodeGeneratorContext,
    ICodeFormatter codeFormatter) : IRootActionParametersCallbackQueryElementsJsGenerator
{
    public StringBuilder GenerateJs(StringBuilder jsBuilder, QueryElementStrategy queryElementStrategy, IEnumerable<JsElementObjectName> jsParameterObjectNames, int indentCount)
        => jsBuilder.AppendLine(codeFormatter.Indent(GenerateQueryElementsJsCode(queryElementStrategy, jsParameterObjectNames), indentCount));

    private string GenerateQueryElementsJsCode(QueryElementStrategy queryElementStrategy, IEnumerable<JsElementObjectName> jsParameterObjectNames)
        => queryElementsJsCodeGeneratorContext
            .GetStrategy(queryElementStrategy)
            .GenerateJsCode(jsParameterObjectNames, string.Empty);
}
