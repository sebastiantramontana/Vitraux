namespace Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;

internal class GetTemplateCall : IGetTemplateCall
{
    public string Generate(string id)
        => $"globalThis.vitraux.storedElements.getTemplate('{id}')";
}
