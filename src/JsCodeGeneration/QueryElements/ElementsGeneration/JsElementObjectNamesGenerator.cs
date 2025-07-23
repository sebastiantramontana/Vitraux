using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Selectors;
using Vitraux.Modeling.Data.Selectors.Collections;
using Vitraux.Modeling.Data.Selectors.Values;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

internal class JsElementObjectNamesGenerator(
    IUniqueSelectorsFilter uniqueSelectorsFilter,
    INotImplementedCaseGuard notImplementedCaseGuard) : IJsElementObjectNamesGenerator
{
    private int _numberedNamePosfixCounter = -1;
    const string ElementObjectNamePrefix = "e";
    const string InsertedElementObjectNamePrefix = "f";
    const string CollectionElementObjectNamePrefix = "c";

    public IEnumerable<JsObjectName> Generate(string namePrefix, ModelMappingData modelMappingData)
    {
        var selectors = uniqueSelectorsFilter.FilterDistinct(modelMappingData);

        return selectors.Select((selector) =>
        {
            var initialName = GetInitialNameBySelector(selector);
            return new JsObjectName(GenerateObjectName(namePrefix, initialName), selector);
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

    private string GenerateObjectName(string namePrefix, string initialName)
    {
        var numberedNamePosfix = Interlocked.Increment(ref _numberedNamePosfixCounter);

        return string.IsNullOrWhiteSpace(namePrefix)
                ? $"{initialName}{numberedNamePosfix}"
                : $"{namePrefix}_{initialName}{numberedNamePosfix}";
    }
}