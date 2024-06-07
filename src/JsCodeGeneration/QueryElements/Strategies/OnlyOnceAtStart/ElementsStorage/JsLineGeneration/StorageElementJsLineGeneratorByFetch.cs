using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

internal class StorageElementJsLineGeneratorByFetch(
    IGetFetchedElementCall getFetchedElementCall,
    IStoragePopulatingElementJsLineGenerator storagePopulatingElementJsLineGenerator)
    : IStorageElementJsLineGeneratorByFetch
{
    public string Generate(PopulatingElementObjectName fetchedObjectName, string parentObjectToAppend)
    {
        var fetchSelector = fetchedObjectName!.AssociatedSelector as ElementFetchSelectorUri;
        var storedElementCall = $"{getFetchedElementCall.Generate(fetchSelector!.Uri, fetchedObjectName.Name)};";

        return storagePopulatingElementJsLineGenerator.Generate(storedElementCall, fetchedObjectName, parentObjectToAppend);
    }
}