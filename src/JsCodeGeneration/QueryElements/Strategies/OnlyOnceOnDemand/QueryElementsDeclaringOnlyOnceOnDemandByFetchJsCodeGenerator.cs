using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandByFetchJsCodeGenerator(
    IGetFetchedElementCall getFetchedElementCall,
    IQueryPopulatingCallingJsBuiltInFunctionCodeGenerator queryElementsDeclaringByPopulatingCallingJsBuilt,
    IJsQueryPopulatingElementsDeclaringOnlyOnceOnDemandGeneratorContext queryGeneratorContext)
    : IQueryElementsDeclaringOnlyOnceOnDemandByFetchJsCodeGenerator
{
    public string GenerateJsCode(string parentElementObjectName, ElementObjectName elementObjectName)
        => queryElementsDeclaringByPopulatingCallingJsBuilt.GenerateJsCode(elementObjectName, getFetchedElementCall.Generate((elementObjectName.AssociatedSelector as ElementFetchSelectorUri).Uri, elementObjectName.Name), queryGeneratorContext);
}