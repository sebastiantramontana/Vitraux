using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal class StorageElementJsLineGeneratorElementsById(
    IStorageElementJsLineGeneratorById generatorById,
    INotImplementedCaseGuard notImplementedSelector)
    : IStorageElementJsLineGeneratorElementsById
{
    public string Generate(JsElementObjectName jsElementObjectName)
        => jsElementObjectName.AssociatedSelector switch
        {
            ElementIdSelectorString idSelectorString => generatorById.Generate(jsElementObjectName.Name, idSelectorString.Id),
            _ => notImplementedSelector.ThrowException<string>(jsElementObjectName.AssociatedSelector)
        };
}
