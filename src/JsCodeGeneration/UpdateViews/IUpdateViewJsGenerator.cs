using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.UpdateViews;

internal interface IUpdateViewJsGenerator
{
    string GenerateJs(QueryElementStrategy queryElementStrategy, JsObjectNamesGrouping objectNamesGrouping, string parentObjectName, string parentElementObjectName);
}
