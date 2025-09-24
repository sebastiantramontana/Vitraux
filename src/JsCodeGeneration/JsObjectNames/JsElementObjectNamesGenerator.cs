using Vitraux.Helpers;
using Vitraux.Modeling.Data.Selectors;
using Vitraux.Modeling.Data.Selectors.Collections;
using Vitraux.Modeling.Data.Selectors.Values;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.JsObjectNames;

internal class JsElementObjectNamesGenerator(
    IUniqueSelectorsFilter uniqueSelectorsFilter,
    IJsObjectNamesGenerator jsObjectNamesGenerator,
    INotImplementedCaseGuard notImplementedCaseGuard) : IJsElementObjectNamesGenerator
{
    const string ElementObjectNamePrefix = "e";
    const string InsertedElementObjectNamePrefix = "f";
    const string CollectionElementObjectNamePrefix = "c";

    public IEnumerable<JsElementObjectName> Generate(string namePrefix, ModelMappingData modelMappingData)
    {
        var selectors = uniqueSelectorsFilter.FilterDistinct(modelMappingData);

        return selectors.Select((selector) =>
        {
            var initialName = GetInitialNameBySelector(selector);
            var jsName = jsObjectNamesGenerator.GenerateUniqueObjectName(namePrefix, initialName);

            return new JsElementObjectName(jsName, selector);
        }).ToArray();
    }

    private string GetInitialNameBySelector(SelectorBase selector)
        => selector switch
        {
            ElementSelectorBase => ElementObjectNamePrefix,
            InsertElementSelectorBase => InsertedElementObjectNamePrefix,
            InsertionSelectorBase => CollectionElementObjectNamePrefix,
            _ => notImplementedCaseGuard.ThrowException<string>(selector)
        };
}