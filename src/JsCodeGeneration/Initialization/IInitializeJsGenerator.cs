using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.Initialization;

internal interface IInitializeJsGenerator
{
    string GenerateJs(IEnumerable<JsElementObjectName> jsObjectNames, string parentObjectName);
}
