namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal interface IUpdateValueByInsertingElementsCall
{
    string Generate(string elementToInsertObjectName, string appendToElemenstObjectName, string queryChildrenFunctionCall, string updateChildElementsFunctionCall);
}
