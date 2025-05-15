using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsAlwaysJsCodeGenerator(
    IQueryElementsDeclaringAlwaysCodeGenerator generator,
    IQueryElementsJsGenerator builder)
    : IQueryElementsAlwaysJsCodeGenerator
{
    public string GenerateJsCode(IEnumerable<JsObjectName> jsObjectNames, string parentElementObjectName)
        => builder.GenerateJsCode(generator, jsObjectNames, parentElementObjectName);
}