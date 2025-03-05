using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart;

internal class QueryElementsOnlyOnceAtStartJsCodeGenerator(
    IQueryElementsJsCodeBuilder builder,
    IQueryElementsDeclaringOnlyOnceAtStartJsCodeGenerator declaringGenerator,
    IQueryElementsOnlyOnceAtStartup initializer
    ) : IQueryElementsOnlyOnceAtStartJsCodeGenerator
{
    public string GenerateJsCode(IEnumerable<ElementObjectName> elements, IEnumerable<CollectionElementObjectName> collectionElementObjectNames, string parentElementObjectName)
    {
        initializer.StoreElementsInAdvance(elements, collectionElementObjectNames, parentElementObjectName);

        return builder.BuildJsCode(declaringGenerator, elements, parentElementObjectName);
    }
}


