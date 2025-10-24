using System.Text;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal class RootActionParametersCallbackBodyJsGenerator(
    IRootActionParametersCallbackQueryElementsJsGenerator callbackQueryElementsJsGenerator,
    IRootActionParametersCallbackConstArgsJsGenerator callbackConstArgsJsGenerator,
    IRootActionParametersCallbackInputValueParameterJsGenerator callbackInputValueParameterJsGenerator,
    IRootActionParametersCallbackReturnArgsJsGenerator callbackReturnArgsJsGenerator) : IRootActionParametersCallbackBodyJsGenerator
{
    public StringBuilder GenerateJs(StringBuilder jsBuilder, ActionData action, QueryElementStrategy queryElementStrategy, IEnumerable<JsElementObjectName> jsParameterObjectNames, string callbackEventParameterName, int indentCount)
    {
        const string ArgsJsObjectName = "args";

        return jsBuilder
                .TryAddTwoLines(callbackQueryElementsJsGenerator.GenerateJs, queryElementStrategy, jsParameterObjectNames, indentCount)
                .AddTwoLines(callbackConstArgsJsGenerator.GenerateJs, ArgsJsObjectName, action, jsParameterObjectNames, indentCount)
                .TryAddTwoLines(callbackInputValueParameterJsGenerator.GenerateJs, action.PassInputValueParameter, ArgsJsObjectName, callbackEventParameterName, indentCount)
                .Add(callbackReturnArgsJsGenerator.GenerateJs, ArgsJsObjectName, indentCount);
    }
}
