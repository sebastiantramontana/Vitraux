using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;

namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal class ToChildQueryFunctionCall(IGetElementsByQuerySelectorCall getElementsByQuerySelectorCall) 
    : IToChildQueryFunctionCall
{
    public string Generate(string toChildQuerySelector)
    {
        const string contentAsParentName = "content";
        return $"({contentAsParentName}) => {getElementsByQuerySelectorCall.Generate(contentAsParentName, toChildQuerySelector)}";
    }
}
