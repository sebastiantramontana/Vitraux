using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

namespace Vitraux.JsCodeGeneration.Initialization;

internal class InitializeJsGeneratorContext(
    IOnlyOnceAtStartInitializeJsGenerator onlyOnceAtStart,
    IOnlyOnceOnDemandInitializeJsGenerator onlyOnceOnDemand,
    IAlwaysInitializeJsGenerator always,
    INotImplementedCaseGuard notImplementedCaseGuard)
    : IInitializeJsGeneratorContext
{
    public IInitializeJsGenerator GetStrategy(QueryElementStrategy strategy)
        => strategy switch
        {
            QueryElementStrategy.OnlyOnceAtStart => onlyOnceAtStart,
            QueryElementStrategy.OnlyOnceOnDemand => onlyOnceOnDemand,
            QueryElementStrategy.Always => always,
            _ => notImplementedCaseGuard.ThrowException<IInitializeJsGenerator>(strategy)
        };
}