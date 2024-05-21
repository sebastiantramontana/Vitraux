using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator(IGetElementsByQuerySelectorCall getElementsByQuerySelectorCalling) : IQueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
        => $"const {elementObjectName.Name} = {getElementsByQuerySelectorCalling.Generate(parentObjectName, elementObjectName.AssociatedSelector.Value)};";
}
