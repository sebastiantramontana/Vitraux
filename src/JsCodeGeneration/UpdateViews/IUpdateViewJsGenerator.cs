using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.UpdateViews;

internal interface IUpdateViewJsGenerator
{
    UpdateViewInfo GenerateJs(QueryElementStrategy queryElementStrategy, JsObjectNamesGrouping objectNamesGrouping, string parentObjectName, string parentElementObjectName);
}
