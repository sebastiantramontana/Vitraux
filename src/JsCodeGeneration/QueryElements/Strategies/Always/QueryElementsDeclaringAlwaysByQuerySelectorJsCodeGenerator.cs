using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator(IGetElementsByQuerySelectorCall getElementsByQuerySelectorCalling) : IQueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
        => $"const {elementObjectName.JsObjName} = {getElementsByQuerySelectorCalling.Generate(parentObjectName, (elementObjectName.AssociatedSelector as ElementQuerySelectorString).Query)};";
}
