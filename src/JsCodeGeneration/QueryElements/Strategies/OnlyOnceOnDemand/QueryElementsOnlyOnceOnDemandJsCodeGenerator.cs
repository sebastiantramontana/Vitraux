using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsOnlyOnceOnDemandJsCodeGenerator(
    IQueryElementsJsCodeBuilder builder,
    IQueryElementsDeclaringOnlyOnceOnDemandJsCodeGenerator generator)
    : IQueryElementsOnlyOnceOnDemandJsCodeGenerator
{
    public string GenerateJsCode(IEnumerable<ElementObjectName> elements, string parentElementObjectName)
        => builder.BuildJsCode(generator, elements, parentElementObjectName);
}