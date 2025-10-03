using System.Text;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.Actions;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.Actions;

internal interface IRootActionsJsGenerator
{
    string GenerateJs(string vmKey, IEnumerable<ActionData> actions, IEnumerable<JsElementObjectName> jsElementObjectNames, QueryElementStrategy queryElementStrategy);
}

internal class RootActionsJsGenerator(
    IRootActionInputElementsQueryJsGenerator rootActionInputElementsQueryJsGenerator,
    IRootActionOnlyOnceAtStartParameterInitQueryJsGenerator rootActionOnlyOnceAtStartParameterInitQueryJsGenerator,
    IRootActionEventRegistrationJsGenerator rootActionEventRegistrationJsGenerator) : IRootActionsJsGenerator
{
    public string GenerateJs(string vmKey, IEnumerable<ActionData> actions, IEnumerable<JsElementObjectName> jsElementObjectNames, QueryElementStrategy queryElementStrategy)
    {
        var jsInputObjectNames = GetInputJsElementObjectNames(actions, jsElementObjectNames);
        var jsParameterObjectNames = GetParameterJsElementObjectNames(actions, jsElementObjectNames);
        var stringBuilder = rootActionInputElementsQueryJsGenerator.GenerateJs(new StringBuilder(), jsInputObjectNames);

        if (queryElementStrategy == QueryElementStrategy.OnlyOnceAtStart)
        {
            stringBuilder = rootActionOnlyOnceAtStartParameterInitQueryJsGenerator.GenerateJs(stringBuilder, jsParameterObjectNames);
        }

        stringBuilder = rootActionEventRegistrationJsGenerator.GenerateJs(stringBuilder, actions, vmKey, jsInputObjectNames);

        return stringBuilder
            .ToString()
            .TrimEnd();
    }

    private static IEnumerable<JsElementObjectName> GetInputJsElementObjectNames(IEnumerable<ActionData> actions, IEnumerable<JsElementObjectName> jsElementObjectNames)
        => FilterInputJsElementObjectNames(GetInputSelectorFromActions(actions), jsElementObjectNames);

    private static IEnumerable<JsElementObjectName> FilterInputJsElementObjectNames(IEnumerable<ElementSelectorBase> inputSelectors, IEnumerable<JsElementObjectName> jsElementObjectNames)
        => jsElementObjectNames.Where(elem => inputSelectors.Contains(elem.AssociatedSelector));

    private static IEnumerable<ElementSelectorBase> GetInputSelectorFromActions(IEnumerable<ActionData> actions)
        => actions.SelectMany(a => a.Targets.Select(t => t.Selector));

    private static IEnumerable<JsElementObjectName> GetParameterJsElementObjectNames(IEnumerable<ActionData> actions, IEnumerable<JsElementObjectName> jsElementObjectNames)
        => FilterParameterJsElementObjectNames(GetParameterSelectorFromActions(actions), jsElementObjectNames);

    private static IEnumerable<JsElementObjectName> FilterParameterJsElementObjectNames(IEnumerable<ElementSelectorBase> parameterSelectors, IEnumerable<JsElementObjectName> jsElementObjectNames)
        => jsElementObjectNames.Where(elem => parameterSelectors.Contains(elem.AssociatedSelector));

    private static IEnumerable<ElementSelectorBase> GetParameterSelectorFromActions(IEnumerable<ActionData> actions)
        => actions.SelectMany(a => a.Targets.SelectMany(t => t.Parameters.Select(p => p.Selector)));
}

internal interface IRootActionEventRegistrationJsGenerator
{
    StringBuilder GenerateJs(StringBuilder stringBuilder, IEnumerable<ActionData> actions, string vmKey, IEnumerable<JsElementObjectName> jsInputObjectNames);
}

internal class RootActionEventRegistrationJsGenerator(
    IRegisterActionAsyncCall registerActionAsyncCall,
    IRegisterActionSyncCall registerActionSyncCall,
    IAtomicAutoNumberGenerator atomicAutoNumberGenerator) : IRootActionEventRegistrationJsGenerator
{
    public StringBuilder GenerateJs(StringBuilder stringBuilder, IEnumerable<ActionData> actions, string vmKey, IEnumerable<JsElementObjectName> jsInputObjectNames)
        => actions.Aggregate(stringBuilder, (sb, action) => RegisterAction(sb, action, vmKey, jsInputObjectNames));

    private StringBuilder RegisterAction(StringBuilder stringBuilder, ActionData actionData, string vmKey, IEnumerable<JsElementObjectName> jsInputObjectNames)
        => actionData.Targets.Aggregate(stringBuilder, (sb, target) =>
            {
                var actionArgsCallback = GenerateActionArgsCallback(target.Parameters);
                var actionKey = GenerateActionKey();
                var targetEventRgistrationJs = GenerateActionTargetEventRegistration(target, actionData.IsAsync, vmKey, actionKey, actionArgsCallback.FunctionName, jsInputObjectNames);

                return sb
                    .AppendLine(actionArgsCallback.JsCode)
                    .TryAppendLineForReadability()
                    .AppendLine(targetEventRgistrationJs);
            });

    private string GenerateActionKey()
        => $"a{atomicAutoNumberGenerator.Next()}";

    private string GenerateActionTargetEventRegistration(ActionTarget actionTarget, bool isAsync, string vmKey, string actionKey, string functionCallbackName, IEnumerable<JsElementObjectName> jsInputObjectNames)
    {
        var jsObjName = jsInputObjectNames.Single(i => i.AssociatedSelector == actionTarget.Selector);
        var registerActionCall = GetRegisterActionCall(isAsync);

        return registerActionCall.Generate(jsObjName.Name, actionTarget.Event, vmKey, actionKey, functionCallbackName);
    }

    private IRegisterActionCall GetRegisterActionCall(bool isAsync)
        => isAsync ? registerActionAsyncCall : registerActionSyncCall;

    private static FunctionCallbackInfo GenerateActionArgsCallback(IEnumerable<ActionParameter> actionParameters)
        => throw new NotImplementedException();
}
