using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal class StorageElementJsLineGeneratorElementsById(IStorageElementJsLineGeneratorById generatorById) : IStorageElementJsLineGeneratorElementsById
{
    public string Generate(JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            ElementIdSelectorString idSelectorString => generatorById.Generate(jsObjectName.Name, idSelectorString.Id),
            ElementIdSelectorDelegate => string.Empty,
            _ => throw new NotImplementedException($"Selector type {jsObjectName.AssociatedSelector} not implemented in {GetType().FullName}"),
        };
}
