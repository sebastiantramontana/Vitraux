using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandByTemplateJsCodeGenerator(
    IGetStoredTemplateCall getStoredElementByTemplateAsArrayCall,
    IQueryTemplateCallingJsBuiltInFunctionCodeGenerator queryElementsDeclaringByTemplateCallingJsBuilt,
    IJsQueryFromTemplateElementsDeclaringOnlyOnceOnDemandGeneratorFactory queryGeneratorFactory)
    : IQueryElementsDeclaringOnlyOnceOnDemandByTemplateJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
        => queryElementsDeclaringByTemplateCallingJsBuilt.GenerateJsCode(elementObjectName, () => getStoredElementByTemplateAsArrayCall.Generate((elementObjectName.AssociatedSelector as ElementTemplateSelectorString).TemplateId, elementObjectName.Name), queryGeneratorFactory);
}