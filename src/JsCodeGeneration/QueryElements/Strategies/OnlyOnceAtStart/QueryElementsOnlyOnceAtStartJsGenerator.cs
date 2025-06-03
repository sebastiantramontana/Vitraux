using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart;

internal class QueryElementsOnlyOnceAtStartJsGenerator(
    IQueryElementsJsGenerator builder,
    IQueryElementsDeclaringOnlyOnceAtStartJsGenerator declaringGenerator
    ) : IQueryElementsOnlyOnceAtStartJsGenerator
{
    public string GenerateJsCode(IEnumerable<JsObjectName> jsObjectNames, string parentElementObjectName)
        => builder.GenerateJsCode(declaringGenerator, jsObjectNames, parentElementObjectName);
}

