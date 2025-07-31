using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Collections;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Collections;

internal class StorageElementCollectionJsLineGeneratorByUri(
    IStorageElementJsLineGeneratorByUri storageElementJsLineGeneratorByUri,
    INotImplementedCaseGuard notImplementedSelector)
    : IStorageElementCollectionJsLineGeneratorByUri
{
    public string Generate(JsObjectName collectionObjectName)
        => collectionObjectName.AssociatedSelector switch
        {
            UriInsertionSelectorUri uriSelector => storageElementJsLineGeneratorByUri.Generate(uriSelector.Uri, collectionObjectName.Name),
            _ => notImplementedSelector.ThrowException<string>(collectionObjectName.AssociatedSelector)
        };
}
