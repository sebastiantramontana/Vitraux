using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Models;

namespace Vitraux.JsCodeGeneration.Values;

internal class TargetElementDirectUpdateValueJsCodeGenerator(
    IElementPlaceAttributeJsCodeGenerator attributeGenerator,
    IElementPlaceContentJsCodeGenerator contentGenerator,
    ICodeFormatting codeFormatting)
    : ITargetElementDirectUpdateValueJsCodeGenerator
{
    public string GenerateJsCode(TargetElement targetElement, IEnumerable<ElementObjectName> associatedElements, string valueObjectName)
        => associatedElements
            .Aggregate(new StringBuilder(), (sb, ae) => sb.AppendLine(GeneratePlaceJsCode(targetElement.Place, ae.Name, valueObjectName)))
            .ToString();

    private string GeneratePlaceJsCode(ElementPlace elementPlace, string elementObjectName, string valueObjectName)
        => elementPlace.ElementPlacing switch
        {
            ElementPlacing.Attribute => codeFormatting.Indent(attributeGenerator.Generate(elementPlace.Value, elementObjectName, valueObjectName)),
            ElementPlacing.Content => codeFormatting.Indent(contentGenerator.Generate(elementObjectName, valueObjectName)),
            _ => throw new NotImplementedException($"ElementPlacing {elementPlace.ElementPlacing} not implemented in TargetElementJsCodeGeneration"),
        };
}
