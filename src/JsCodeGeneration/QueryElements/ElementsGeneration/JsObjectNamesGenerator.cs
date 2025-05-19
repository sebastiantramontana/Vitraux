using Vitraux.Helpers;
using Vitraux.Modeling.Data.Selectors;
using Vitraux.Modeling.Data.Selectors.Collections;
using Vitraux.Modeling.Data.Selectors.Values;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

internal class JsObjectNamesGenerator(INotImplementedCaseGuard notImplementedSelector) : IJsObjectNamesGenerator
{
    public IEnumerable<JsObjectName> Generate(string namePrefix, IEnumerable<SelectorBase> selectors)
        => selectors
            .Select((selector, indexAsPostfix) =>
            {
                var readableName = GetReadableNameBySelector(selector);
                return new JsObjectName(GenerateObjectName(namePrefix, readableName, indexAsPostfix), selector);
            })
            .RunNow();

    private string GetReadableNameBySelector(SelectorBase selector)
        => selector switch
        {
            ElementSelectorBase => "element",
            InsertElementSelectorBase => "from",
            InsertionSelectorBase => "coll",
            _ => notImplementedSelector.ThrowException<string>(selector)
        };

    private static string GenerateObjectName(string namePrefix, string readableName, int indexAsPostfix)
        => $"{namePrefix}_{readableName}{indexAsPostfix}";
}