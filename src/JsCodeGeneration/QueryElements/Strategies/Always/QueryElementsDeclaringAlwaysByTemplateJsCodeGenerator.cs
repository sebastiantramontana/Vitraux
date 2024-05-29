using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysByTemplateJsCodeGenerator(
    IGetElementByTemplateAsArrayCall getElementByTemplateAsArrayCalling,
    IQueryTemplateCallingJsBuiltInFunctionCodeGenerator queryElementsDeclaringByTemplateCallingJsBuilt,
    IJsQueryFromTemplateElementsDeclaringAlwaysGeneratorFactory queryGeneratorFactory)
    : IQueryElementsDeclaringAlwaysByTemplateJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
        => queryElementsDeclaringByTemplateCallingJsBuilt.GenerateJsCode(elementObjectName, () => getElementByTemplateAsArrayCalling.Generate((elementObjectName.AssociatedSelector as ElementTemplateSelectorString).TemplateId), queryGeneratorFactory);
}
