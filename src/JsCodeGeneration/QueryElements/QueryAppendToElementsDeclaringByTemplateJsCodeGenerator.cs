using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal class QueryAppendToElementsDeclaringByTemplateJsCodeGenerator : IQueryAppendToElementsDeclaringByTemplateJsCodeGenerator
{
    public string GenerateAppendToJsCode(string appendToObjectName, PopulatingAppendToElementSelector elementToAppend, IJsQueryFromTemplateElementsDeclaringGeneratorFactory jsGeneratorFactory)
        => jsGeneratorFactory
            .GetInstance(elementToAppend.SelectionBy)
            .GenerateJsCode("document", MapElementObjectNameFromTemplateSelector(appendToObjectName, elementToAppend));

    private static ElementObjectName MapElementObjectNameFromTemplateSelector(string objectName, PopulatingAppendToElementSelector fromTemplateElementSelector)
    {
        var newSelector = CreateElementSelectorFromTemplateSelector(fromTemplateElementSelector);
        return new ElementObjectName(objectName, newSelector);
    }

    private static ElementSelector CreateElementSelectorFromTemplateSelector(PopulatingAppendToElementSelector selector)
        => selector.SelectionBy switch
        {
            PopulatingAppendToElementSelection.Id => new ElementIdSelector(selector.Value),
            PopulatingAppendToElementSelection.QuerySelector => new ElementQuerySelector(selector.Value),
            _ => throw new NotImplementedException($"{selector.SelectionBy} not implemented"),
        };
}
