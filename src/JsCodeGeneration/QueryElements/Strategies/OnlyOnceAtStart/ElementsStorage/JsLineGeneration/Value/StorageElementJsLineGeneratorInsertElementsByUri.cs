using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal class StorageElementJsLineGeneratorInsertElementsByUri(
    IStorageElementJsLineGeneratorByUri lineGeneratorByUri,
    INotImplementedCaseGuard notImplementedSelector)
    : IStorageElementJsLineGeneratorInsertElementsByUri
{
    public string Generate(JsElementObjectName fetchedJsObjectName)
        => fetchedJsObjectName.AssociatedSelector switch
        {
            InsertElementUriSelectorUri uriSelector => lineGeneratorByUri.Generate(uriSelector.Uri, fetchedJsObjectName.Name),
            _ => notImplementedSelector.ThrowException<string>(fetchedJsObjectName.AssociatedSelector)
        };
}
