using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysByQuerySelectorJsGenerator(IGetElementsByQuerySelectorCall getElementsByQuerySelectorCalling)
    : IQueryElementsDeclaringAlwaysByQuerySelectorJsGenerator
{
    public string GenerateJsCode(string parentElementObjectName, JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            ElementQuerySelectorString elementQuerySelector => GenerateJsLineByQueryString(jsObjectName.Name, parentElementObjectName, elementQuerySelector.Query),
            ElementQuerySelectorDelegate => string.Empty,
            _ => throw new NotImplementedException($"Selector type {jsObjectName.AssociatedSelector} not implemented in {GetType().FullName}"),
        };

    private string GenerateJsLineByQueryString(string jsObjectName, string parentElementObjectName, string query)
        => $"const {jsObjectName} = {getElementsByQuerySelectorCalling.Generate(parentElementObjectName, query)};";
}
