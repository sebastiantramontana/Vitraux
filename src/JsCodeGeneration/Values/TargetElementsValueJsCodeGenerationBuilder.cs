using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;
using Vitraux.Modeling.Models;

namespace Vitraux.JsCodeGeneration.Values;

internal class TargetElementsValueJsCodeGenerationBuilder(
    ITargetElementDirectUpdateValueJsCodeGenerator targetElementDirectJsCodeGenerator,
    ITargetByPopulatingElementsUpdateValueJsCodeGenerator targetPopulatingJsCodeGenerator)
    : ITargetElementsValueJsCodeGenerationBuilder
{
    public string Build(string parentValueObjectName, ValueObjectName value, IEnumerable<ElementObjectName> elements)
        => value
            .AssociatedValue
            .TargetElements
            .Aggregate(new StringBuilder(), (sb, te) =>
            {
                var associatedElements = GetElementNamesAssociatedToTargetElement(elements, te);
                var generator = GetCodeGeneratorBySelector(te.Selector);

                return sb
                    .AppendLine(generator.GenerateJsCode(te, associatedElements, parentValueObjectName, value.Name))
                    .AppendLine();
            })
            .ToString()
            .TrimEnd();

    private static IEnumerable<ElementObjectName> GetElementNamesAssociatedToTargetElement(IEnumerable<ElementObjectName> elements, TargetElement targetElement)
        => elements.Where(e => e.AssociatedSelector == targetElement.Selector);

    private ITargetElementUpdateValueJsCodeGenerator GetCodeGeneratorBySelector(ElementSelectorBase selector)
        => (selector is PopulatingElementSelectorBase) ? targetPopulatingJsCodeGenerator : targetElementDirectJsCodeGenerator;

}
