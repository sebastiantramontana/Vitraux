namespace Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;

internal class GetStoredElementByTemplateAsArrayCall : IGetStoredElementByTemplateAsArrayCall
{
    public string Generate(string id, string elementObjectName)
        => $"globalThis.vitraux.storedElements.getStoredElementByTemplateAsArray('{id}', '{elementObjectName}')";
}
