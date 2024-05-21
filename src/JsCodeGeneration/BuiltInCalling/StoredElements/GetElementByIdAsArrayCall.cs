namespace Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements
{
    internal class GetElementByIdAsArrayCall : IGetElementByIdAsArrayCall
    {
        public string Generate(string id)
            => $"globalThis.vitraux.storedElements.getElementByIdAsArray('{id}')";
    }
}
