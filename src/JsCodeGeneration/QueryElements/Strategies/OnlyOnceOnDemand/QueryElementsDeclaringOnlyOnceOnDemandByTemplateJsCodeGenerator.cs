using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandByTemplateJsCodeGenerator(
    IGetStoredTemplateCall getStoredElementByTemplateCall,
    IQueryPopulatingCallingJsBuiltInFunctionCodeGenerator queryElementsDeclaringByPopulatingCallingJsBuilt,
    IJsQueryPopulatingElementsDeclaringOnlyOnceOnDemandGeneratorContext queryGeneratorContext)
    : IQueryElementsDeclaringOnlyOnceOnDemandByTemplateJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
        => queryElementsDeclaringByPopulatingCallingJsBuilt.GenerateJsCode(elementObjectName, getStoredElementByTemplateCall.Generate((elementObjectName.AssociatedSelector as InsertElementTemplateSelectorString).TemplateId, elementObjectName.Name), queryGeneratorContext);
}
