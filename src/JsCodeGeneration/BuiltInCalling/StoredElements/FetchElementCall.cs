namespace Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;

internal class FetchElementCall : IFetchElementCall
{
    public string Generate(Uri uri)
        => $"await globalThis.vitraux.storedElements.fetchElement('{uri}')";
}