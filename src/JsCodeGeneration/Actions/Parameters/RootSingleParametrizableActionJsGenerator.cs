using System.Text;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal class RootSingleParametrizableActionJsGenerator(
    IRootActionInputElementsQueryJsGenerator rootActionInputElementsQueryJsGenerator,
    IActionInputJsElementObjectNamesFilter actionInputJsElementObjectNames,
    IActionParameterJsElementObjectNamesFilter actionParameterJsElementObjectNames,
    IRootActionOnlyOnceAtStartParameterInitQueryJsGenerator rootActionOnlyOnceAtStartParameterInitQueryJsGenerator,
    IRootParametrizableActionInputEventsRegistrationJsGenerator rootParametrizableActionInputEventsRegistrationJsGenerator,
    IRootActionParametersCallbackJsGenerator rootActionParametersCallbackJsGenerator,
    IActionParametersCallbackFunctionNameGenerator callbackFunctionNameGenerator) : IRootSingleParametrizableActionJsGenerator
{
    public StringBuilder GenerateJs(StringBuilder jsBuilder, string vmKey, ActionData action, IEnumerable<JsElementObjectName> jsAllElementObjectNames, QueryElementStrategy queryElementStrategy)
    {
        var jsInputObjectNames = actionInputJsElementObjectNames.Filter(action, jsAllElementObjectNames);
        var jsParameterObjectNames = actionParameterJsElementObjectNames.Filter(action, jsAllElementObjectNames);
        var callbackFunctionName = callbackFunctionNameGenerator.Generate();

        return jsBuilder
            .AddTwoLines(rootActionInputElementsQueryJsGenerator.GenerateJs, jsInputObjectNames)
            .TryAddTwoLines(TryGenerateParametersQuery, queryElementStrategy, jsParameterObjectNames)
            .AddTwoLines(rootActionParametersCallbackJsGenerator.GenerateJs, callbackFunctionName, action, queryElementStrategy, jsParameterObjectNames)
            .Add(rootParametrizableActionInputEventsRegistrationJsGenerator.GenerateJs, action, jsInputObjectNames, vmKey, callbackFunctionName);
    }

    private StringBuilder TryGenerateParametersQuery(StringBuilder jsBuilder, QueryElementStrategy queryElementStrategy, IEnumerable<JsElementObjectName> jsParameterObjectNames)
        => queryElementStrategy == QueryElementStrategy.OnlyOnceAtStart
            ? rootActionOnlyOnceAtStartParameterInitQueryJsGenerator.GenerateJs(jsBuilder, jsParameterObjectNames)
            : jsBuilder;
}