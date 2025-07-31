using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Collections;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Collections;

internal class StorageElementCollectionJsLineGeneratorByTemplate(
    IStorageElementJsLineGeneratorByTemplate storageElementJsLineGeneratorByTemplate,
    INotImplementedCaseGuard notImplementedSelector)
    : IStorageElementCollectionJsLineGeneratorByTemplate
{
    public string Generate(JsObjectName collectionObjectName)
        => collectionObjectName.AssociatedSelector switch
        {
            TemplateInsertionSelectorId templateSelector => storageElementJsLineGeneratorByTemplate.Generate(templateSelector.TemplateId, collectionObjectName.Name),
            _ => notImplementedSelector.ThrowException<string>(collectionObjectName.AssociatedSelector)
        };
}
