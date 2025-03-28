using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal class TargetElementDirectUpdateValueJsCodeGenerator(
    IElementPlaceAttributeJsCodeGenerator attributeGenerator,
    IElementPlaceContentJsCodeGenerator contentGenerator)
    : ITargetElementDirectUpdateValueJsCodeGenerator
{
    public string GenerateJsCode(ElementTarget targetElement, IEnumerable<ElementObjectName> associatedElements, string parentValueObjectName, string valueObjectName)
        => associatedElements
            .Aggregate(new StringBuilder(), (sb, ae) => sb.AppendLine(GeneratePlaceJsCode(targetElement.Place, ae.Name, parentValueObjectName, valueObjectName)))
            .ToString()
            .TrimEnd();

    private string GeneratePlaceJsCode(ElementPlace elementPlace, string elementObjectName, string parentValueObjectName, string valueObjectName)
        => elementPlace switch
        {
            AttributeElementPlace attrPlace => attributeGenerator.Generate(attrPlace.Attribute, elementObjectName, parentValueObjectName, valueObjectName),
            ContentElementPlace => contentGenerator.Generate(elementObjectName, parentValueObjectName, valueObjectName),
            _ => throw new NotImplementedException($"ElementPlace {elementPlace.GetType().Name} not implemented in TargetElementJsCodeGeneration"),
        };
}
