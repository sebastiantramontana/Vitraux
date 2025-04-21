using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;
using Vitraux.Modeling.Data.Values;

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

    private static IEnumerable<ElementObjectName> GetElementNamesAssociatedToTargetElement(IEnumerable<ElementObjectName> elements, ElementValueTarget targetElement)
        => elements.Where(e => e.AssociatedSelector == targetElement.Selector);

    private ITargetElementUpdateValueJsCodeGenerator GetCodeGeneratorBySelector(ElementSelectorBase selector)
        => (selector is InsertElementSelectorBase) ? targetPopulatingJsCodeGenerator : targetElementDirectJsCodeGenerator;

}
