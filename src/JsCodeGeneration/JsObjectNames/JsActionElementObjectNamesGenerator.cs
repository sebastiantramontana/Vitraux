using Vitraux.Helpers;
using Vitraux.Modeling.Data.Actions;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.JsObjectNames;

internal class JsActionElementObjectNamesGenerator(
    IJsObjectNamesGenerator jsObjectNamesGenerator,
    INotImplementedCaseGuard notImplementedCaseGuard) : IJsActionElementObjectNamesGenerator
{
    private enum ActionSelectorLabel
    {
        Input,
        Parameter
    }

    const string InputObjectNamePrefix = "i";
    const string ParameterObjectNamePrefix = "p";

    public IEnumerable<JsElementObjectName> Generate(string namePrefix, IEnumerable<ActionData> actions)
    {
        var selectors = GetUniqueSelectors(actions);

        return selectors.Select((selector) =>
        {
            var initialName = GetInitialNameByLabel(selector.Label);
            var jsName = jsObjectNamesGenerator.GenerateUniqueObjectName(namePrefix, initialName);
            return new JsElementObjectName(jsName, selector.Selector);
        }).ToArray();
    }

    private string GetInitialNameByLabel(ActionSelectorLabel label)
        => label switch
        {
            ActionSelectorLabel.Input => InputObjectNamePrefix,
            ActionSelectorLabel.Parameter => ParameterObjectNamePrefix,
            _ => notImplementedCaseGuard.ThrowException<string>(label)
        };

    private static IEnumerable<(ElementSelectorBase Selector, ActionSelectorLabel Label)> GetUniqueSelectors(IEnumerable<ActionData> actions)
        => actions
            .SelectMany(a =>
                a.Targets
                .SelectMany(t =>
                    t.Parameters
                    .Select(p => (p.Selector, ActionSelectorLabel.Parameter))
                    .Append((t.Selector, ActionSelectorLabel.Input))))
            .DistinctBy(a => a.Selector);
}
