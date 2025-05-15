using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysCodeGenerator(IJsQueryElementsDeclaringAlwaysGeneratorContext context) : IQueryElementsDeclaringAlwaysCodeGenerator
{
    public string GenerateJsCode(string parentElementObjectName, JsObjectName jsObjectName)
        => context
            .GetStrategy(jsObjectName.AssociatedSelector)
            .GenerateJsCode(parentElementObjectName, jsObjectName);
}
