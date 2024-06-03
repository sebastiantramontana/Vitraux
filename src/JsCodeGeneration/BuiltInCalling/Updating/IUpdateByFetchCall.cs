namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal interface IUpdateByFetchCall
{
    string Generate(string fetchedElementObjectName, string appendToElementsObjectName, string toChildQueryFunctionCall, string updateFetchedElementChildFunctionCall);
}