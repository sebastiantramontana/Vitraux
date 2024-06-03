namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal interface IUpdateByTemplateCall
{
    string Generate(string templateElementObjectName, string appendToElementsObjectName, string toChildQueryFunctionCall, string updateTemplateChildFunctionCall);
}