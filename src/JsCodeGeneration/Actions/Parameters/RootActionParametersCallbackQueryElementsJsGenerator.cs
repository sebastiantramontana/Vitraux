using System.Text;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal class RootActionParametersCallbackQueryElementsJsGenerator(
    IQueryElementsJsCodeGeneratorContext queryElementsJsCodeGeneratorContext,
    ICodeFormatter codeFormatter) : IRootActionParametersCallbackQueryElementsJsGenerator
{
    private const string RootElementObjectName = "document";

    public StringBuilder GenerateJs(StringBuilder jsBuilder, QueryElementStrategy queryElementStrategy, IEnumerable<JsElementObjectName> jsParameterObjectNames, int indentCount)
        => jsParameterObjectNames.Any()
            ? jsBuilder.Append(codeFormatter.Indent(GenerateQueryElementsJsCode(queryElementStrategy, jsParameterObjectNames), indentCount))
            : jsBuilder;

    private string GenerateQueryElementsJsCode(QueryElementStrategy queryElementStrategy, IEnumerable<JsElementObjectName> jsParameterObjectNames)
        => queryElementsJsCodeGeneratorContext
            .GetStrategy(queryElementStrategy)
            .GenerateJsCode(jsParameterObjectNames, RootElementObjectName);
}
