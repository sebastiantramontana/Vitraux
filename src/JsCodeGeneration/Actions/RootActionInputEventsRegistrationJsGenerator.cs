using System.Text;
using Vitraux.JsCodeGeneration.BuiltInCalling.Actions;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.Actions;

internal class RootActionInputEventsRegistrationJsGenerator(
    IRegisterActionAsyncCall registerActionAsyncCall,
    IRegisterActionSyncCall registerActionSyncCall) : IRootActionInputEventsRegistrationJsGenerator
{
    public StringBuilder GenerateJs(StringBuilder jsBuilder, ActionData action, IEnumerable<JsElementObjectName> jsInputObjectNames, string vmKey, string actionArgsCallbackFunctionName)
        => action.Targets.Aggregate(jsBuilder, (sb, target) => sb.AppendLine(GenerateActionTargetEventRegistration(target, jsInputObjectNames, vmKey, actionArgsCallbackFunctionName)));

    private string GenerateActionTargetEventRegistration(ActionTarget actionTarget, IEnumerable<JsElementObjectName> jsInputObjectNames, string vmKey, string actionArgsCallbackFunctionName)
    {
        var registerActionCall = GetRegisterActionCall(actionTarget.Parent.IsAsync);
        var jsInputObjectName = GetInputObjectName(jsInputObjectNames, actionTarget.Selector);

        return registerActionCall.Generate(jsInputObjectName, actionTarget.Events, vmKey, actionTarget.Parent.ActionKey, actionArgsCallbackFunctionName);
    }

    private IRegisterActionCall GetRegisterActionCall(bool isAsync)
        => isAsync ? registerActionAsyncCall : registerActionSyncCall;

    private static string GetInputObjectName(IEnumerable<JsElementObjectName> jsInputObjectNames, ElementSelectorBase targetSelector)
        => jsInputObjectNames.Single(n => n.AssociatedSelector == targetSelector).Name;
}
