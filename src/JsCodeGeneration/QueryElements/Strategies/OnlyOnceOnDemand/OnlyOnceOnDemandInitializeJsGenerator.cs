using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class OnlyOnceOnDemandInitializeJsGenerator(IPromiseJsGenerator promiseJsGenerator) : IOnlyOnceOnDemandInitializeJsGenerator
{
    public string GenerateJs(IEnumerable<JsElementObjectName> _, string __)
        => promiseJsGenerator.ReturnResolvedPromiseJsLine;
}