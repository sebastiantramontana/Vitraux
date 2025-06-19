using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration;

internal interface IRootJsGenerator
{
    GeneratedJsCode GenerateJs(FullObjectNames fullObjectNames, IEnumerable<JsObjectName> allJsElementObjectNames, QueryElementStrategy queryElementStrategy);
}
