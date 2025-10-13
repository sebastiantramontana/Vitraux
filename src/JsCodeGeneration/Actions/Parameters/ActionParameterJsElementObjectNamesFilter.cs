using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal class ActionParameterJsElementObjectNamesFilter : IActionParameterJsElementObjectNamesFilter
{
    public IEnumerable<JsElementObjectName> Filter(ActionData action, IEnumerable<JsElementObjectName> jsAllElementObjectNames)
        => FilterParameterJsElementObjectNames(GetParameterSelectorFromActions(action), jsAllElementObjectNames);

    private static IEnumerable<JsElementObjectName> FilterParameterJsElementObjectNames(IEnumerable<ElementSelectorBase> parameterSelectors, IEnumerable<JsElementObjectName> jsAllElementObjectNames)
       => jsAllElementObjectNames.Where(elem => parameterSelectors.Contains(elem.AssociatedSelector));

    private static IEnumerable<ElementSelectorBase> GetParameterSelectorFromActions(ActionData action)
        => action.Parameters.Select(p => p.Selector);
}
