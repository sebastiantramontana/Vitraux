namespace Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;

internal class GetElementsByQuerySelectorCall : IGetElementsByQuerySelectorCall
{
    public string Generate(string parentObjectName, string querySelector)
        => $"globalThis.vitraux.storedElements.getElementsByQuerySelector({parentObjectName}, '{querySelector}')";
}
