using System.Text;
using Vitraux.JsCodeGeneration.Actions.Parameters;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.Actions;

internal class RootSingleActionJsGenerator(
    IRootActionInputElementsQueryJsGenerator rootActionInputElementsQueryJsGenerator,
    IRootActionOnlyOnceAtStartParameterInitQueryJsGenerator rootActionOnlyOnceAtStartParameterInitQueryJsGenerator,
    IRootActionInputEventsRegistrationJsGenerator rootActionInputEventsRegistrationJsGenerator,
    IRootActionParametersCallbackJsGenerator rootActionParametersCallbackJsGenerator,
    IActionParametersCallbackFunctionNameGenerator callbackFunctionNameGenerator) : IRootSingleActionJsGenerator
{
    public StringBuilder GenerateJs(StringBuilder jsBuilder, string vmKey, ActionData action, IEnumerable<JsElementObjectName> jsAllElementObjectNames, QueryElementStrategy queryElementStrategy)
    {
        var jsInputObjectNames = GetInputJsElementObjectNames(action, jsAllElementObjectNames);
        var jsParameterObjectNames = GetParameterJsElementObjectNames(action, jsAllElementObjectNames);
        var callbackFunctionName = callbackFunctionNameGenerator.Generate();

        return jsBuilder
            .Add(rootActionInputElementsQueryJsGenerator.GenerateJs, jsInputObjectNames)
            .Add(GenerateParametersQuery, queryElementStrategy, jsParameterObjectNames)
            .Add(rootActionParametersCallbackJsGenerator.GenerateJs, callbackFunctionName, action, queryElementStrategy, jsParameterObjectNames)
            .Add(rootActionInputEventsRegistrationJsGenerator.GenerateJs, action, jsInputObjectNames, vmKey, callbackFunctionName);
    }

    private StringBuilder GenerateParametersQuery(StringBuilder jsBuilder, QueryElementStrategy queryElementStrategy, IEnumerable<JsElementObjectName> jsParameterObjectNames)
        => queryElementStrategy == QueryElementStrategy.OnlyOnceAtStart
            ? rootActionOnlyOnceAtStartParameterInitQueryJsGenerator.GenerateJs(jsBuilder, jsParameterObjectNames)
            : jsBuilder;

    private static IEnumerable<JsElementObjectName> GetInputJsElementObjectNames(ActionData action, IEnumerable<JsElementObjectName> jsAllElementObjectNames)
        => FilterInputJsElementObjectNames(GetInputSelectorFromAction(action), jsAllElementObjectNames);

    private static IEnumerable<JsElementObjectName> FilterInputJsElementObjectNames(IEnumerable<ElementSelectorBase> inputSelectors, IEnumerable<JsElementObjectName> jsAllElementObjectNames)
        => jsAllElementObjectNames.Where(elem => inputSelectors.Contains(elem.AssociatedSelector));

    private static IEnumerable<ElementSelectorBase> GetInputSelectorFromAction(ActionData action)
        => action.Targets.Select(t => t.Selector);

    private static IEnumerable<JsElementObjectName> GetParameterJsElementObjectNames(ActionData action, IEnumerable<JsElementObjectName> jsAllElementObjectNames)
        => FilterParameterJsElementObjectNames(GetParameterSelectorFromActions(action), jsAllElementObjectNames);

    private static IEnumerable<JsElementObjectName> FilterParameterJsElementObjectNames(IEnumerable<ElementSelectorBase> parameterSelectors, IEnumerable<JsElementObjectName> jsAllElementObjectNames)
        => jsAllElementObjectNames.Where(elem => parameterSelectors.Contains(elem.AssociatedSelector));

    private static IEnumerable<ElementSelectorBase> GetParameterSelectorFromActions(ActionData action)
        => action.Parameters.Select(p => p.Selector);
}
