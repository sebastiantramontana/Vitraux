using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;

internal class QueryElementsOnlyOnceAtStartup(IStoreElementsJsCodeBuilder codeBuilder, IJsCodeExecutor codeExecutor) : IQueryElementsOnlyOnceAtStartup
{
    public void StoreElementsInAdvance(IEnumerable<ElementObjectName> elements, IEnumerable<CollectionElementObjectName> collectionElementObjectNames, string parentObjectName)
        => codeExecutor.Excute(codeBuilder.Build(elements, parentObjectName));
}