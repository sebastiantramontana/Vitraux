using System.Text;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal class RootActionParametersCallbackQueryElementsJsGenerator(IQueryElementsJsCodeGeneratorContext queryElementsJsCodeGeneratorContext) : IRootActionParametersCallbackQueryElementsJsGenerator
{
    private const string RootElementObjectName = "document";

    public StringBuilder GenerateJs(StringBuilder jsBuilder, QueryElementStrategy queryElementStrategy, IEnumerable<JsElementObjectName> jsParameterObjectNames, int indentCount)
        => jsParameterObjectNames.Any()
            ? jsBuilder.Add(GenerateQueryElementsJsCode, queryElementStrategy, jsParameterObjectNames, indentCount)
            : jsBuilder;

    private StringBuilder GenerateQueryElementsJsCode(StringBuilder jsBuilder, QueryElementStrategy queryElementStrategy, IEnumerable<JsElementObjectName> jsParameterObjectNames, int indentCount)
        => queryElementsJsCodeGeneratorContext
            .GetStrategy(queryElementStrategy)
            .GenerateJsCode(jsBuilder, jsParameterObjectNames, RootElementObjectName, indentCount);
}
