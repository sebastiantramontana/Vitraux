using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class OnlyOnceOnDemandInitializeJsGenerator(IPromiseJsGenerator promiseJsGenerator) : IOnlyOnceOnDemandInitializeJsGenerator
{
    public string GenerateJs(IEnumerable<JsObjectName> _, string __)
        => promiseJsGenerator.ReturnResolvedPromiseJsLine;
}