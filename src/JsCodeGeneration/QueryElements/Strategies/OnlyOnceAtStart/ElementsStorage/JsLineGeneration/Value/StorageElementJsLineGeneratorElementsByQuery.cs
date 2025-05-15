using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal class StorageElementJsLineGeneratorElementsByQuery(IStorageElementJsLineGeneratorByQuerySelector generatorByQuerySelector) : IStorageElementJsLineGeneratorElementsByQuery
{
    public string Generate(JsObjectName jsObjectName, string parentObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            ElementQuerySelectorString querySelectorString => generatorByQuerySelector.Generate(jsObjectName.Name, querySelectorString.Query, parentObjectName),
            ElementQuerySelectorDelegate => string.Empty,
            _ => throw new NotImplementedException($"Selector type {jsObjectName.AssociatedSelector} not implemented in {GetType().FullName}"),
        };
}
