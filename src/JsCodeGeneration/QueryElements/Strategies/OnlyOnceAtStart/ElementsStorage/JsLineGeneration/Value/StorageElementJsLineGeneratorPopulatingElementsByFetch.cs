using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal class StorageElementJsLineGeneratorPopulatingElementsByFetch(
    IGetFetchedElementCall getFetchedElementCall,
    IStoragePopulatingElementJsLineGenerator storagePopulatingElementJsLineGenerator)
    : IStorageElementJsLineGeneratorPopulatingElementsByFetch
{
    public string Generate(PopulatingElementObjectName fetchedObjectName, string parentObjectToAppend)
    {
        var fetchSelector = fetchedObjectName!.AssociatedSelector as ElementFetchSelectorUri;
        var storedElementCall = $"{getFetchedElementCall.Generate(fetchSelector!.Uri, fetchedObjectName.Name)};";

        return storagePopulatingElementJsLineGenerator.Generate(storedElementCall, fetchedObjectName, parentObjectToAppend);
    }
}