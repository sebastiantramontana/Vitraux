using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandByFetchJsCodeGenerator(
    IGetFetchedElementCall getFetchedElementCall,
    IQueryPopulatingCallingJsBuiltInFunctionCodeGenerator queryElementsDeclaringByPopulatingCallingJsBuilt,
    IJsQueryPopulatingElementsDeclaringOnlyOnceOnDemandGeneratorContext queryGeneratorContext)
    : IQueryElementsDeclaringOnlyOnceOnDemandByFetchJsCodeGenerator
{
    public string GenerateJsCode(string parentElementObjectName, ElementObjectName elementObjectName)
        => queryElementsDeclaringByPopulatingCallingJsBuilt.GenerateJsCode(elementObjectName, getFetchedElementCall.Generate((elementObjectName.AssociatedSelector as InsertElementUriSelectorUri).Uri, elementObjectName.JsObjName), queryGeneratorContext);
}