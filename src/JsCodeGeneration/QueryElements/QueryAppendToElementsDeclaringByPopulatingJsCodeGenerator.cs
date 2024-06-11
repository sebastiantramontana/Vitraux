using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal class QueryAppendToElementsDeclaringByPopulatingJsCodeGenerator : IQueryAppendToElementsDeclaringByPopulatingJsCodeGenerator
{
    public string GenerateAppendToJsCode(string appendToObjectName, PopulatingAppendToElementSelectorBase elementToAppend, IJsQueryPopulatingElementsDeclaringGeneratorContext jsGeneratorContext)
        => jsGeneratorContext
            .GetStrategy(elementToAppend.SelectionBy)
            .GenerateJsCode("document", MapElementObjectNameFromPopulatingSelector(appendToObjectName, elementToAppend));

    private static ElementObjectName MapElementObjectNameFromPopulatingSelector(string objectName, PopulatingAppendToElementSelectorBase fromTemplateElementSelector)
    {
        var newSelector = CreateElementSelectorFromPopulatingSelector(fromTemplateElementSelector);
        return new ElementObjectName(objectName, newSelector);
    }

    private static ElementSelectorBase CreateElementSelectorFromPopulatingSelector(PopulatingAppendToElementSelectorBase selector)
        => selector.SelectionBy switch
        {
            PopulatingAppendToElementSelection.Id => new ElementIdSelectorString((selector as PopulatingAppendToElementIdSelectorString).Id),
            PopulatingAppendToElementSelection.QuerySelector => new ElementQuerySelectorString((selector as PopulatingAppendToElementQuerySelectorString).Query),
            _ => throw new NotImplementedException($"{selector.SelectionBy} not implemented"),
        };
}
