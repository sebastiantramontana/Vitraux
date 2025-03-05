namespace Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements
{
    internal interface IGetStoredElementsByQuerySelectorCall
    {
        string Generate(string parentElementObjectName, string querySelector, string elementObjectName);
    }
}