using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

internal class StorageElementJsLineGeneratorByFetch(IGetFetchedElementCall getFetchedElementCall)
    : IStorageElementJsLineGeneratorByFetch
{
    public string Generate(Uri uri, string elementObjectName)
        => $"{getFetchedElementCall.Generate(uri, elementObjectName)};";
}
