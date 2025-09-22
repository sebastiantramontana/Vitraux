using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsAlwaysJsCodeGenerator(
    IQueryElementsDeclaringAlwaysCodeGenerator generator,
    IQueryElementsJsGenerator builder)
    : IQueryElementsAlwaysJsCodeGenerator
{
    public string GenerateJsCode(IEnumerable<JsElementObjectName> jsObjectNames, string parentElementObjectName)
        => builder.GenerateJsCode(generator, jsObjectNames, parentElementObjectName);
}