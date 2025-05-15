using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;

internal class QueryElementsOnlyOnceAtStartup(
    IStoreElementsJsCodeGenerator codeGenerator,
    IJsCodeExecutor codeExecutor) : IQueryElementsOnlyOnceAtStartup
{
    public void StoreElementsInAdvance(IEnumerable<JsObjectName> jsObjectNames, string parentObjectName)
        => codeExecutor.Excute(codeGenerator.Generate(jsObjectNames, parentObjectName));
}