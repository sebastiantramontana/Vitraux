namespace Vitraux.JsCodeGeneration.Values;

internal interface IElementPlaceContentJsCodeGenerator
{
    string Generate(string elementObjectName, string parentValueObjectName, string valueObjectName);
}
