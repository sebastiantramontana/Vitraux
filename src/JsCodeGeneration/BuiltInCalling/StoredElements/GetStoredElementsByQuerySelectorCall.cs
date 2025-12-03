namespace Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;

internal class GetStoredElementsByQuerySelectorCall : IGetStoredElementsByQuerySelectorCall
{
    public string Generate(string parentObjectName, string querySelector, string elementObjectName)
        => $"globalThis.vitraux.storedElements.getStoredElementsByQuerySelector({parentObjectName}, '{EscapeQuerySelector(querySelector)}', '{elementObjectName}')";

    private static string EscapeQuerySelector(string querySelector)
        => querySelector.Replace("'", @"\'");
}
