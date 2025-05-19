using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal class StorageElementJsLineGeneratorInsertElementsByUri(
    IStorageElementJsLineGeneratorByUri lineGeneratorByUri,
    INotImplementedCaseGuard notImplementedSelector)
    : IStorageElementJsLineGeneratorInsertElementsByUri
{
    public string Generate(JsObjectName fetchedJsObjectName)
        => fetchedJsObjectName.AssociatedSelector switch
        {
            InsertElementUriSelectorUri uriSelector => lineGeneratorByUri.Generate(uriSelector.Uri, fetchedJsObjectName.Name),
            InsertElementUriSelectorDelegate => string.Empty,
            _ => notImplementedSelector.ThrowException<string>(fetchedJsObjectName.AssociatedSelector)
        };
}
