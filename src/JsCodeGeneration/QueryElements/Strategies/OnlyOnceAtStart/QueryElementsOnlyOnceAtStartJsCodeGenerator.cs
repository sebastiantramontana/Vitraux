using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart;

internal class QueryElementsOnlyOnceAtStartJsCodeGenerator(
    IQueryElementsJsCodeBuilder builder,
    IQueryElementsDeclaringOnlyOnceAtStartJsCodeGenerator declaringGenerator,
    IQueryElementsOnlyOnceAtStartup initializer
    ) : IQueryElementsOnlyOnceAtStartJsCodeGenerator
{
    public string GenerateJsCode(IEnumerable<ElementObjectName> elements, string parentObjectName)
    {
        initializer.StoreElementsInAdvance(elements, parentObjectName);

        return builder.BuildJsCode(declaringGenerator, elements, parentObjectName);
    }
}


