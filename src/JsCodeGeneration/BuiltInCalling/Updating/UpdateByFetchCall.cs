namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal class UpdateByFetchCall(IUpdateByPopulatingElementsCall updateByPopulatingElementsCall) : IUpdateByFetchCall
{
    public string Generate(string fetchedElementObjectName, string appendToElementsObjectName, string toChildQueryFunctionCall, string updateFetchedElementChildFunctionCall)
        => updateByPopulatingElementsCall.Generate(fetchedElementObjectName, appendToElementsObjectName, toChildQueryFunctionCall, updateFetchedElementChildFunctionCall);
}
