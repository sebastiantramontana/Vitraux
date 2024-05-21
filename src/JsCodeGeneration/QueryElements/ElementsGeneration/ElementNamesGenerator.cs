using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

internal class ElementNamesGenerator : IElementNamesGenerator
{
    public IEnumerable<ElementObjectName> Generate(IEnumerable<ElementSelector> selectors)
        => selectors.Select((selector, indexAsPostfix) => CreateElementObjectName(indexAsPostfix, selector));

    private static ElementObjectName CreateElementObjectName(int indexAsPostfix, ElementSelector selector)
    {
        var objectName = $"elements{indexAsPostfix}";

        return selector is ElementTemplateSelector templateSelector
            ? new ElementTemplateObjectName(objectName, $"{objectName}_appendTo", templateSelector)
            : new ElementObjectName(objectName, selector);
    }
}

