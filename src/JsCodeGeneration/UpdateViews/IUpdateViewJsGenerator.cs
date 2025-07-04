using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.UpdateViews;

internal interface IUpdateViewJsGenerator
{
    string GenerateJs(QueryElementStrategy queryElementStrategy, FullObjectNames fullObjectNames, string parentObjectName, string parentElementObjectName);
}
