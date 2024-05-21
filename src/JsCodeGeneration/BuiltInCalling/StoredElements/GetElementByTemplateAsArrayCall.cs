namespace Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements
{
    internal class GetElementByTemplateAsArrayCall : IGetElementByTemplateAsArrayCall
    {
        public string Generate(string id)
            => $"globalThis.vitraux.storedElements.getElementByTemplateAsArray('{id}')";
    }
}
