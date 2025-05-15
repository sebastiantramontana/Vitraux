using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysByIdJsGenerator(IGetElementByIdAsArrayCall getElementByIdAsArrayCalling) 
    : IQueryElementsDeclaringAlwaysByIdJsGenerator
{
    public string GenerateJsCode(string parentElementObjectName, JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            ElementIdSelectorString elementIdSelector => GenerateJsLineByIdString(jsObjectName.Name, elementIdSelector.Id),
            ElementIdSelectorDelegate => string.Empty,
            _ => throw new NotImplementedException($"Selector type {jsObjectName.AssociatedSelector} not implemented in {GetType().FullName}"),
        };

    private string GenerateJsLineByIdString(string jsObjectName, string id)
        => $"const {jsObjectName} = {getElementByIdAsArrayCalling.Generate(id)};";
}
