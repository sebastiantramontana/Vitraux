namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal interface IUpdateValueByPopulatingElementsCall
{
    string Generate(string elementObjectName, string appendToElementsObjectName, string toChildQueryFunctionCall, string updateElementChildFunctionCall);
}
