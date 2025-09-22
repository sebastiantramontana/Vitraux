using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class AlwaysInitializeJsGenerator(IPromiseJsGenerator promiseJsGenerator) : IAlwaysInitializeJsGenerator
{
    public string GenerateJs(IEnumerable<JsElementObjectName> _, string __)
        => promiseJsGenerator.ReturnResolvedPromiseJsLine;
}