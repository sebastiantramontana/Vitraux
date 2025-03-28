using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Elements;
using Vitraux.Modeling.Data.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal class QueryAppendToElementsDeclaringByPopulatingJsCodeGenerator : IQueryAppendToElementsDeclaringByPopulatingJsCodeGenerator
{
    public string GenerateAppendToJsCode(string appendToObjectName, PopulatingAppendToElementSelectorBase elementToAppend, IJsQueryPopulatingElementsDeclaringGeneratorContext jsGeneratorContext)
        => jsGeneratorContext
            .GetStrategy(elementToAppend)
            .GenerateJsCode("document", MapElementObjectNameFromPopulatingSelector(appendToObjectName, elementToAppend));

    private static ElementObjectName MapElementObjectNameFromPopulatingSelector(string objectName, PopulatingAppendToElementSelectorBase fromTemplateElementSelector)
    {
        var newSelector = CreateElementSelectorFromPopulatingSelector(fromTemplateElementSelector);
        return new ElementObjectName(objectName, newSelector);
    }

    private static ElementSelectorBase CreateElementSelectorFromPopulatingSelector(PopulatingAppendToElementSelectorBase selector)
        => selector switch
        {
            PopulatingAppendToElementIdSelectorString => new ElementIdSelectorString((selector as PopulatingAppendToElementIdSelectorString).Id),
            PopulatingAppendToElementQuerySelectorString => new ElementQuerySelectorString((selector as PopulatingAppendToElementQuerySelectorString).Query),
            _ => throw new NotImplementedException($"{selector.GetType().Name} not implemented"),
        };
}
