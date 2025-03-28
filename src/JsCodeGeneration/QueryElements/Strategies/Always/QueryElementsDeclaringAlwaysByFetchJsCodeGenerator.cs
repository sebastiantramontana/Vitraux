using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysByFetchJsCodeGenerator(
    IFetchElementCall fetchElementCall,
    IQueryPopulatingCallingJsBuiltInFunctionCodeGenerator queryElementsDeclaringByPopulatingCallingJsBuilt,
    IJsQueryPopulatingElementsDeclaringAlwaysGeneratorContext queryGeneratorContext)
    : IQueryElementsDeclaringAlwaysByFetchJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
        => queryElementsDeclaringByPopulatingCallingJsBuilt.GenerateJsCode(elementObjectName, fetchElementCall.Generate((elementObjectName.AssociatedSelector as InsertElementUriSelectorUri).Uri), queryGeneratorContext);
}
