using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandByQuerySelectorJsCodeGenerator(IGetStoredElementsByQuerySelectorCall getStoredElementsByQuerySelectorCalling) : IQueryElementsDeclaringOnlyOnceOnDemandByQuerySelectorJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
        => $"const {elementObjectName.Name} = {getStoredElementsByQuerySelectorCalling.Generate(parentObjectName, elementObjectName.AssociatedSelector.Value, elementObjectName.Name)};";
}
