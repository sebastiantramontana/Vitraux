using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.Initialization;

internal interface IInitializeJsGenerator
{
    string GenerateJs(IEnumerable<JsObjectName> jsObjectNames, string parentObjectName);
}
