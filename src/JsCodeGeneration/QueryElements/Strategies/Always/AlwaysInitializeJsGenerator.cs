using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class AlwaysInitializeJsGenerator(IPromiseJsGenerator promiseJsGenerator) : IAlwaysInitializeJsGenerator
{
    public string GenerateJs(IEnumerable<JsObjectName> _, string __)
        => promiseJsGenerator.ReturnResolvedPromiseJsLine;
}