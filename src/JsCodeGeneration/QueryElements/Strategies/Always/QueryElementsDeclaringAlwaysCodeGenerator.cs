using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysCodeGenerator(IJsQueryElementsDeclaringAlwaysGeneratorFactory factory) : IQueryElementsDeclaringAlwaysCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
        => factory
            .GetInstance(elementObjectName.AssociatedSelector)
            .GenerateJsCode(parentObjectName, elementObjectName);
}
