using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysCodeGenerator(IJsQueryElementsDeclaringAlwaysGeneratorContext context) : IQueryElementsDeclaringAlwaysCodeGenerator
{
    public string GenerateJsCode(string parentElementObjectName, JsElementObjectName jsElementObjectName)
        => context
            .GetStrategy(jsElementObjectName.AssociatedSelector)
            .GenerateJsCode(parentElementObjectName, jsElementObjectName);
}
