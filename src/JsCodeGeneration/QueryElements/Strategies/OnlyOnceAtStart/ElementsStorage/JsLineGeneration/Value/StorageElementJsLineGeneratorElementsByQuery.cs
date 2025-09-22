using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal class StorageElementJsLineGeneratorElementsByQuery(
    IStorageElementJsLineGeneratorByQuerySelector generatorByQuerySelector,
    INotImplementedCaseGuard notImplementedSelector) 
    : IStorageElementJsLineGeneratorElementsByQuery
{
    public string Generate(JsElementObjectName jsElementObjectName, string parentObjectName)
        => jsElementObjectName.AssociatedSelector switch
        {
            ElementQuerySelectorString querySelectorString => generatorByQuerySelector.Generate(jsElementObjectName.Name, querySelectorString.Query, parentObjectName),
            _ => notImplementedSelector.ThrowException<string>(jsElementObjectName.AssociatedSelector)
        };
}
