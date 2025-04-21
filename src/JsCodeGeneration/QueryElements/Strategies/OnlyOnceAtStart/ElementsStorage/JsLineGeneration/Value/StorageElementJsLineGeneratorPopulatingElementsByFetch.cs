using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal class StorageElementJsLineGeneratorPopulatingElementsByFetch(
    IGetFetchedElementCall getFetchedElementCall,
    IStoragePopulatingElementJsLineGenerator storagePopulatingElementJsLineGenerator)
    : IStorageElementJsLineGeneratorPopulatingElementsByFetch
{
    public string Generate(InsertElementObjectName fetchedObjectName, string parentObjectToAppend)
    {
        var fetchSelector = fetchedObjectName!.AssociatedSelector as InsertElementUriSelectorUri;
        var storedElementCall = $"{getFetchedElementCall.Generate(fetchSelector!.Uri, fetchedObjectName.JsObjName)};";

        return storagePopulatingElementJsLineGenerator.Generate(storedElementCall, fetchedObjectName, parentObjectToAppend);
    }
}