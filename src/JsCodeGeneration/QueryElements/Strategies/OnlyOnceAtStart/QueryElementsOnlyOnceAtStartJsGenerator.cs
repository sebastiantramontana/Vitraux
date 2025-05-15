using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart;

internal class QueryElementsOnlyOnceAtStartJsGenerator(
    IQueryElementsJsGenerator builder,
    IQueryElementsDeclaringOnlyOnceAtStartJsGenerator declaringGenerator,
    IQueryElementsOnlyOnceAtStartup initializer
    ) : IQueryElementsOnlyOnceAtStartJsGenerator
{
    public string GenerateJsCode(IEnumerable<JsObjectName> jsObjectNames, string parentElementObjectName)
    {
        initializer.StoreElementsInAdvance(jsObjectNames, parentElementObjectName);

        return builder.GenerateJsCode(declaringGenerator, jsObjectNames, parentElementObjectName);
    }
}

