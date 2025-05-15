namespace Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;

internal class GetStoredTemplateCall : IGetStoredTemplateCall
{
    public string Generate(string id, string jsElementObjectName)
        => $"globalThis.vitraux.storedElements.getStoredTemplate('{id}', '{jsElementObjectName}')";
}
