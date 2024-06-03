namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal interface IUpdateByPopulatingElementsCall
{
    string Generate(string elementObjectName, string appendToElementsObjectName, string toChildQueryFunctionCall, string updateElementChildFunctionCall);
}
