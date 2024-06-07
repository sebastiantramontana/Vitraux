namespace Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;

internal class GetFetchedElementCall : IGetFetchedElementCall
{
    public string Generate(Uri uri, string elementObjectName)
        => $"await globalThis.vitraux.storedElements.getFetchedElement('{uri}', '{elementObjectName}')";
}
