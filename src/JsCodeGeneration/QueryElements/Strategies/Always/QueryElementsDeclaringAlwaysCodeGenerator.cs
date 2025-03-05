using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysCodeGenerator(IJsQueryElementsDeclaringAlwaysGeneratorContext context) : IQueryElementsDeclaringAlwaysCodeGenerator
{
    public string GenerateJsCode(string parentElementObjectName, ElementObjectName elementObjectName)
        => context
            .GetStrategy(elementObjectName.AssociatedSelector)
            .GenerateJsCode(parentElementObjectName, elementObjectName);
}
