namespace Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements
{
    internal class GetStoredElementByIdAsArrayCall : IGetStoredElementByIdAsArrayCall
    {
        public string Generate(string id, string elementObjectName)
            => $"globalThis.vitraux.storedElements.getStoredElementByIdAsArray('{id}', '{elementObjectName}')";
    }
}
