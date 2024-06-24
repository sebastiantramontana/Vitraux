namespace Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements
{
    internal class GetStoredElementsByQuerySelectorCall : IGetStoredElementsByQuerySelectorCall
    {
        public string Generate(string parentObjectName, string querySelector, string elementObjectName)
            => $"globalThis.vitraux.storedElements.getStoredElementsByQuerySelector({parentObjectName}, '{querySelector}', '{elementObjectName}')";
    }
}
