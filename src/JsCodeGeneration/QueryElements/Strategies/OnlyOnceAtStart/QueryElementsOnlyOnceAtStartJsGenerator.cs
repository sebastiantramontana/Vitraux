using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart;

internal class QueryElementsOnlyOnceAtStartJsGenerator(
    IQueryElementsJsGenerator builder,
    IQueryElementsDeclaringOnlyOnceAtStartJsGenerator declaringGenerator) 
    : IQueryElementsOnlyOnceAtStartJsGenerator
{
    public string GenerateJsCode(IEnumerable<JsElementObjectName> jsObjectNames, string parentElementObjectName)
        => builder.GenerateJsCode(declaringGenerator, jsObjectNames, parentElementObjectName);
}