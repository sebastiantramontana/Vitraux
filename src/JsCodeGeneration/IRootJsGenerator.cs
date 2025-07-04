using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration;

internal interface IRootJsGenerator
{
    GeneratedJsCode GenerateJs(FullObjectNames fullObjectNames, QueryElementStrategy queryElementStrategy);
}
