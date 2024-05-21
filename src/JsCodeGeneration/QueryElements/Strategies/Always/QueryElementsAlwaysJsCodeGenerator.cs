using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsAlwaysJsCodeGenerator(
    IQueryElementsDeclaringAlwaysCodeGenerator generator,
    IQueryElementsJsCodeBuilder builder)
    : IQueryElementsAlwaysJsCodeGenerator
{
    public string GenerateJsCode(IEnumerable<ElementObjectName> elements, string parentObjectName)
        => builder.BuildJsCode(generator, elements, parentObjectName);
}


