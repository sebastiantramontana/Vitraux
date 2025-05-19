using System.Text;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal class TargetElementsDirectUpdateValueJsGenerator(
    IElementPlaceAttributeJsGenerator attributeGenerator,
    IElementPlaceContentJsGenerator contentGenerator,
    INotImplementedCaseGuard notImplementedPlaceGuard)
    : ITargetElementsDirectUpdateValueJsGenerator
{
    public string GenerateJsCode(ElementValueTarget targetElement, IEnumerable<JsObjectName> associatedObjects, string parentValueObjectName, string valueObjectName)
        => associatedObjects
            .Aggregate(new StringBuilder(), (sb, jsObjName) => sb.AppendLine(GeneratePlaceJsCode(targetElement.Place, jsObjName.Name, parentValueObjectName, valueObjectName)))
            .ToString()
            .TrimEnd();

    private string GeneratePlaceJsCode(ElementPlace elementPlace, string elementObjectName, string parentValueObjectName, string valueObjectName)
        => elementPlace switch
        {
            AttributeElementPlace attrPlace => attributeGenerator.Generate(attrPlace.Attribute, elementObjectName, parentValueObjectName, valueObjectName),
            ContentElementPlace => contentGenerator.Generate(elementObjectName, parentValueObjectName, valueObjectName),
            _ => notImplementedPlaceGuard.ThrowException<string>(elementPlace),
        };
}
