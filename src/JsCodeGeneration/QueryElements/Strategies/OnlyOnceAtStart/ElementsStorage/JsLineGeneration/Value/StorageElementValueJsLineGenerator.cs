using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Selectors.Values;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal class StorageElementValueJsLineGenerator(
    IStorageElementJsLineGeneratorElementsById generatorById,
    IStorageElementJsLineGeneratorElementsByQuery generatorByQuerySelector,
    IStorageElementJsLineGeneratorInsertElementsByTemplate generatorByTemplate,
    IStorageElementJsLineGeneratorInsertElementsByUri generatorByUri,
    INotImplementedCaseGuard notImplementedSelector)
    : IStorageElementValueJsLineGenerator
{
    public string Generate(JsElementObjectName jsElementObjectName, string parentObjectName)
        => jsElementObjectName.AssociatedSelector switch
        {
            ElementIdSelectorBase => generatorById.Generate(jsElementObjectName),
            ElementQuerySelectorBase => generatorByQuerySelector.Generate(jsElementObjectName, parentObjectName),
            InsertElementTemplateSelectorBase => generatorByTemplate.Generate(jsElementObjectName),
            InsertElementUriSelectorBase => generatorByUri.Generate(jsElementObjectName),
            _ => notImplementedSelector.ThrowException<string>(jsElementObjectName.AssociatedSelector)
        };
}
