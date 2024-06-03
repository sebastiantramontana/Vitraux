namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal class UpdateByTemplateCall(IUpdateByPopulatingElementsCall updateByPopulatingElementsCall) : IUpdateByTemplateCall
{
    public string Generate(string templateElementObjectName, string appendToElementsObjectName, string toChildQueryFunctionCall, string updateTemplateChildFunctionCall) 
        => updateByPopulatingElementsCall.Generate(templateElementObjectName, appendToElementsObjectName, toChildQueryFunctionCall, updateTemplateChildFunctionCall);
}
