using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

internal class StorageElementJsLineGeneratorByUri(IGetFetchedElementCall getFetchedElementCall) : IStorageElementJsLineGeneratorByUri
{
    public string Generate(Uri uri, string fetchedJsObjectName) 
        => $"{getFetchedElementCall.Generate(uri, fetchedJsObjectName)};";
}
