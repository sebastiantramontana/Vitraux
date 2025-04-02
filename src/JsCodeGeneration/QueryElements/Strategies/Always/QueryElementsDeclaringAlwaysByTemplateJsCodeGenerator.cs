using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysByTemplateJsCodeGenerator(
    IGetTemplateCall getTemplateCall,
    IQueryPopulatingCallingJsBuiltInFunctionCodeGenerator queryElementsDeclaringByTemplateCallingJsBuilt,
    IJsQueryPopulatingElementsDeclaringAlwaysGeneratorContext queryGeneratorContext)
    : IQueryElementsDeclaringAlwaysByTemplateJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
        => queryElementsDeclaringByTemplateCallingJsBuilt.GenerateJsCode(elementObjectName, getTemplateCall.Generate((elementObjectName.AssociatedSelector as InsertElementTemplateSelectorId).TemplateId), queryGeneratorContext);
}
