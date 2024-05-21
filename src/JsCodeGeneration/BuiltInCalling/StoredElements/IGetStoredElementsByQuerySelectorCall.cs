namespace Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements
{
    internal interface IGetStoredElementsByQuerySelectorCall
    {
        string Generate(string parentObjectName, string querySelector, string elementObjectName);
    }
}