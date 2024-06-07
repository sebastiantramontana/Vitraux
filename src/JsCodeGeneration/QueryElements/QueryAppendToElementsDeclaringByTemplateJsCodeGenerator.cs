using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal class QueryAppendToElementsDeclaringByTemplateJsCodeGenerator : IQueryAppendToElementsDeclaringByTemplateJsCodeGenerator
{
    public string GenerateAppendToJsCode(string appendToObjectName, PopulatingAppendToElementSelectorBase elementToAppend, IJsQueryPopulatingElementsDeclaringGeneratorContext jsGeneratorContext)
        => jsGeneratorContext
            .GetStrategy(elementToAppend.SelectionBy)
            .GenerateJsCode("document", MapElementObjectNameFromTemplateSelector(appendToObjectName, elementToAppend));

    private static ElementObjectName MapElementObjectNameFromTemplateSelector(string objectName, PopulatingAppendToElementSelectorBase fromTemplateElementSelector)
    {
        var newSelector = CreateElementSelectorFromTemplateSelector(fromTemplateElementSelector);
        return new ElementObjectName(objectName, newSelector);
    }

    private static ElementSelectorBase CreateElementSelectorFromTemplateSelector(PopulatingAppendToElementSelectorBase selector)
        => selector.SelectionBy switch
        {
            PopulatingAppendToElementSelection.Id => new ElementIdSelectorString((selector as PopulatingAppendToElementIdSelectorString).Id),
            PopulatingAppendToElementSelection.QuerySelector => new ElementQuerySelectorString((selector as PopulatingAppendToElementQuerySelectorString).Query),
            _ => throw new NotImplementedException($"{selector.SelectionBy} not implemented"),
        };
}
