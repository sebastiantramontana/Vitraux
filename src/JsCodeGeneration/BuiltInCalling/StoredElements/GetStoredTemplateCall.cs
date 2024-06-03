namespace Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;

internal class GetStoredTemplateCall : IGetStoredTemplateCall
{
    public string Generate(string id, string elementObjectName)
        => $"globalThis.vitraux.storedElements.getStoredTemplate('{id}', '{elementObjectName}')";
}
