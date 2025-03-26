namespace Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;

internal interface IGetElementsByQuerySelectorCall
{
    string Generate(string parentElementObjectName, string querySelector);
}