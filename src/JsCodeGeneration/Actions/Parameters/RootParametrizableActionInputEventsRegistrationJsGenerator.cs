using System.Text;
using Vitraux.JsCodeGeneration.BuiltInCalling.Actions.Parametrizables;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal class RootParametrizableActionInputEventsRegistrationJsGenerator(
    IRegisterParametrizableActionAsyncCall registerParametrizableActionAsyncCall,
    IRegisterParametrizableActionSyncCall registerParametrizableActionSyncCall) : IRootParametrizableActionInputEventsRegistrationJsGenerator
{
    public StringBuilder GenerateJs(StringBuilder jsBuilder, ActionData action, IEnumerable<JsElementObjectName> jsInputObjectNames, string vmKey, string actionArgsCallbackFunctionName)
        => action
            .Targets
            .Aggregate(jsBuilder, (sb, target) => sb.AddLine(GenerateActionTargetEventRegistration, target, jsInputObjectNames, vmKey, actionArgsCallbackFunctionName))
            .TrimEnd();

    private StringBuilder GenerateActionTargetEventRegistration(StringBuilder jsBuilder, ActionTarget actionTarget, IEnumerable<JsElementObjectName> jsInputObjectNames, string vmKey, string actionArgsCallbackFunctionName)
    {
        var registerActionCall = GetRegisterActionCall(actionTarget.Parent.IsAsync);
        var jsInputObjectName = GetInputObjectName(jsInputObjectNames, actionTarget.Selector);
        var jsRegisterActionCall = registerActionCall.Generate(jsInputObjectName, actionTarget.Events, vmKey, actionTarget.Parent.ActionKey, actionArgsCallbackFunctionName);
        var RegisterActionCallJsSentence = $"{jsRegisterActionCall};";

        return jsBuilder.Append(RegisterActionCallJsSentence);
    }

    private IRegisterParametrizableActionCall GetRegisterActionCall(bool isAsync)
        => isAsync ? registerParametrizableActionAsyncCall : registerParametrizableActionSyncCall;

    private static string GetInputObjectName(IEnumerable<JsElementObjectName> jsInputObjectNames, ElementSelectorBase targetSelector)
        => jsInputObjectNames.Single(n => n.AssociatedSelector == targetSelector).Name;
}
