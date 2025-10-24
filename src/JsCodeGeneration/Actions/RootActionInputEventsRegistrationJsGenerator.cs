using System.Text;
using Vitraux.JsCodeGeneration.BuiltInCalling.Actions;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.Actions;

internal class RootActionInputEventsRegistrationJsGenerator(
    IRegisterActionAsyncCall registerActionAsyncCall,
    IRegisterActionSyncCall registerActionSyncCall) : IRootActionInputEventsRegistrationJsGenerator
{
    public StringBuilder GenerateJs(StringBuilder jsBuilder, ActionData action, IEnumerable<JsElementObjectName> jsInputObjectNames, string vmKey)
        => action
            .Targets
            .Aggregate(jsBuilder, (sb, target) => sb.AddLine(GenerateActionTargetEventRegistration, target, jsInputObjectNames, vmKey))
            .TrimEnd();

    private StringBuilder GenerateActionTargetEventRegistration(StringBuilder jsBuilder, ActionTarget actionTarget, IEnumerable<JsElementObjectName> jsInputObjectNames, string vmKey)
    {
        var registerActionCall = GetRegisterActionCall(actionTarget.Parent.IsAsync);
        var jsInputObjectName = GetInputObjectName(jsInputObjectNames, actionTarget.Selector);
        var jsRegisterActionCall = registerActionCall.Generate(jsInputObjectName, actionTarget.Events, vmKey, actionTarget.Parent.ActionKey);
        var RegisterActionCallJsSentence = $"{jsRegisterActionCall};";

        return jsBuilder.Append(RegisterActionCallJsSentence);
    }

    private IRegisterActionCall GetRegisterActionCall(bool isAsync)
        => isAsync ? registerActionAsyncCall : registerActionSyncCall;

    private static string GetInputObjectName(IEnumerable<JsElementObjectName> jsInputObjectNames, ElementSelectorBase targetSelector)
        => jsInputObjectNames.Single(n => n.AssociatedSelector == targetSelector).Name;
}
