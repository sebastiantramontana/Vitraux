using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.UpdateViews;

internal interface IUpdateViewJsGenerator
{
    string GenerateJs(QueryElementStrategy queryElementStrategy, FullObjectNames fullObjectNames, IEnumerable<JsObjectName> allJsElementObjectNames, string parentObjectName, string parentElementObjectName);
}
