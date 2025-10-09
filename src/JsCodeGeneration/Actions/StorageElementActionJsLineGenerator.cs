using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.Actions;

internal class StorageElementActionJsLineGenerator(
    IStorageElementJsLineGeneratorElementsById generatorById,
    IStorageElementJsLineGeneratorElementsByQuery generatorByQuerySelector,
    INotImplementedCaseGuard notImplementedSelector)
    : IStorageElementActionJsLineGenerator
{
    private const string ParentElementObjectName = "document";

    public string Generate(JsElementObjectName jsElementObjectName)
        => jsElementObjectName.AssociatedSelector switch
        {
            ElementIdSelectorBase => generatorById.Generate(jsElementObjectName),
            ElementQuerySelectorBase => generatorByQuerySelector.Generate(jsElementObjectName, ParentElementObjectName),
            _ => notImplementedSelector.ThrowException<string>(jsElementObjectName.AssociatedSelector)
        };
}