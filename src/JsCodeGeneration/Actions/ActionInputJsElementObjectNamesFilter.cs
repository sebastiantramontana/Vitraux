using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.Actions;

internal class ActionInputJsElementObjectNamesFilter : IActionInputJsElementObjectNamesFilter
{
    public IEnumerable<JsElementObjectName> Filter(ActionData action, IEnumerable<JsElementObjectName> jsAllElementObjectNames)
        => FilterInputJsElementObjectNames(GetInputSelectorFromAction(action), jsAllElementObjectNames);

    private static IEnumerable<JsElementObjectName> FilterInputJsElementObjectNames(IEnumerable<ElementSelectorBase> inputSelectors, IEnumerable<JsElementObjectName> jsAllElementObjectNames)
        => jsAllElementObjectNames.Where(elem => inputSelectors.Contains(elem.AssociatedSelector));

    private static IEnumerable<ElementSelectorBase> GetInputSelectorFromAction(ActionData action)
        => action.Targets.Select(t => t.Selector);
}
