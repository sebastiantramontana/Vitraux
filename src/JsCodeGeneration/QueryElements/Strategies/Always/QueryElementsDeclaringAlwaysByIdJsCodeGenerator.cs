using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysByIdJsCodeGenerator(IGetElementByIdAsArrayCall getElementByIdAsArrayCalling) : IQueryElementsDeclaringAlwaysByIdJsCodeGenerator
{
    public string GenerateJsCode(string parentElementObjectName, ElementObjectName elementObjectName)
        => $"const {elementObjectName.Name} = {getElementByIdAsArrayCalling.Generate((elementObjectName.AssociatedSelector as ElementIdSelectorIdString).Id)};";
}
