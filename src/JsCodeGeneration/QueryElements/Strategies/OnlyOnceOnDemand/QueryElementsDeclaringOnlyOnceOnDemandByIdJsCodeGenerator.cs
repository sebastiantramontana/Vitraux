using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandByIdJsCodeGenerator(IGetStoredElementByIdAsArrayCall getStoredElementByIdAsArrayCalling) : IQueryElementsDeclaringOnlyOnceOnDemandByIdJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
        => $"const {elementObjectName.Name} = {getStoredElementByIdAsArrayCalling.Generate(elementObjectName.AssociatedSelector.Value, elementObjectName.Name)};";
}
