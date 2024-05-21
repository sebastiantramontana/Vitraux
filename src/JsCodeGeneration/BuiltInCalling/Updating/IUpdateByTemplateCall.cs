namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating
{
    internal interface IUpdateByTemplateCall
    {
        string Generate(string templateElementAsArrayObjectName, string appendToElementsObjectName, string toChildQueryFunctionCall, string updateTemplateChildFunctionCall);
    }
}