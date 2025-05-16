using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal class StorageElementValueJsLineGenerator(
    IStorageElementJsLineGeneratorElementsById generatorById,
    IStorageElementJsLineGeneratorElementsByQuery generatorByQuerySelector,
    IStorageElementJsLineGeneratorInsertElementsByTemplate generatorByTemplate,
    IStorageElementJsLineGeneratorInsertElementsByUri generatorByUri,
    INotImplementedSelector notImplementedSelector)
    : IStorageElementValueJsLineGenerator
{
    public string Generate(JsObjectName jsObjectName, string parentObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            ElementIdSelectorBase => generatorById.Generate(jsObjectName),
            ElementQuerySelectorBase => generatorByQuerySelector.Generate(jsObjectName, parentObjectName),
            InsertElementTemplateSelectorBase => generatorByTemplate.Generate(jsObjectName),
            InsertElementUriSelectorBase => generatorByUri.Generate(jsObjectName),
            _ => notImplementedSelector.ThrowNotImplementedException<string>(jsObjectName.AssociatedSelector)
        };
}
