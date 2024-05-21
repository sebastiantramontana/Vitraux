using Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal class QueryElementsJsCodeGeneratorByStrategyFactory(
    IQueryElementsOnlyOnceAtStartJsCodeGenerator atStartGenerator,
    IQueryElementsOnlyOnceOnDemandJsCodeGenerator onDemandGenerator,
    IQueryElementsAlwaysJsCodeGenerator alwaysGenerator)
    : IQueryElementsJsCodeGeneratorByStrategyFactory
{
    public IQueryElementsJsCodeGenerator GetInstance(QueryElementStrategy strategy)
    => strategy switch
    {
        QueryElementStrategy.OnlyOnceAtStart => atStartGenerator,
        QueryElementStrategy.OnlyOnceOnDemand => onDemandGenerator,
        QueryElementStrategy.Always => alwaysGenerator,
        _ => throw new NotImplementedException($"JS Code Generator not implemented for {strategy} case"),
    };
}
