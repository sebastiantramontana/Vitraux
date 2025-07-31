using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal class StorageElementJsLineGeneratorElementsById(
    IStorageElementJsLineGeneratorById generatorById,
    INotImplementedCaseGuard notImplementedSelector)
    : IStorageElementJsLineGeneratorElementsById
{
    public string Generate(JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            ElementIdSelectorString idSelectorString => generatorById.Generate(jsObjectName.Name, idSelectorString.Id),
            _ => notImplementedSelector.ThrowException<string>(jsObjectName.AssociatedSelector)
        };
}
