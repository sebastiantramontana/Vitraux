using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Collections;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Collections;

internal class StorageElementCollectionJsLineGeneratorByTemplate(IStorageElementJsLineGeneratorByTemplate storageElementJsLineGeneratorByTemplate)
    : IStorageElementCollectionJsLineGeneratorByTemplate
{
    public string Generate(JsObjectName collectionObjectName)
        => collectionObjectName.AssociatedSelector switch
        {
            TemplateInsertionSelectorId templateSelector => storageElementJsLineGeneratorByTemplate.Generate(templateSelector.TemplateId, collectionObjectName.Name),
            TemplateInsertionSelectorDelegate => string.Empty,
            _ => throw new NotImplementedException($"Selector type {collectionObjectName.AssociatedSelector} not implemented in {GetType().FullName}"),
        };
}
